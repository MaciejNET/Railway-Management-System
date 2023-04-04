using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.Commands.Ticket;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Extensions;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IMapper _mapper;
    private readonly IPassengerRepository _passengerRepository;
    private readonly IStationRepository _stationRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly ITripRepository _tripRepository;

    public BookingService(ITripRepository tripRepository, ITicketRepository ticketRepository,
        IStationRepository stationRepository, IPassengerRepository passengerRepository, IMapper mapper)
    {
        _tripRepository = tripRepository;
        _ticketRepository = ticketRepository;
        _stationRepository = stationRepository;
        _passengerRepository = passengerRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TicketDto>> BookTicket(BookTicket bookTicket, int passengerId)
    {
        var trip = await _tripRepository.GetByIdAsync(bookTicket.TripId);

        if (trip is null)
        {
            return Error.NotFound(description: $"Trip with id: '{bookTicket.TripId}' does not exists.");
        }

        if (bookTicket.TripDate < DateOnly.FromDateTime(DateTime.Now))
        {
            return Error.Validation(description: "Cannot book ticket for a past date.");
        }
        
        if (TripExtensions.IsTrainRunsOnGivenDate(trip, bookTicket.TripDate) is false)
        {
            return Error.Validation(description: "This trip does not run on given date.");
        }

        var passenger = await _passengerRepository.GetByIdAsync(passengerId);

        if (passenger is null)
        {
            return Error.NotFound(description: $"Passenger with id: '{passengerId}' does not exists.");
        }

        
        var startStation = await _stationRepository.GetByNameAsync(bookTicket.StartStation);
        if (startStation is null)
        {
            return Error.NotFound($"Station with name: '{bookTicket.StartStation}' does not exists.");
        }
        
        var endStation = await _stationRepository.GetByNameAsync(bookTicket.EndStation);
        if (endStation is null)
        {
            return Error.NotFound($"Station with name: '{bookTicket.EndStation}' does not exists.");
        }
        

        var tripStations = trip.Schedules.Select(x => x.Station).ToList();
        if (!(tripStations.Contains(startStation) && tripStations.Contains(endStation)))
        {
            return Error.Validation(description: "Trip schedule does not contains provided stations");
        }

        var seatsToBook = GetAvailableSeats(trip, bookTicket.TripDate, startStation, endStation).ToList();

        if (seatsToBook.Count == 0)
        {
            return Error.Failure(description: "There is no free seat to book");
        }

        var stations = GetStationsToBook(tripStations, startStation, endStation).ToList();
        stations.Add(endStation);
        
        var passengerDiscount = 0;
        if (passenger.Discount is not null)
        {
            passengerDiscount = passenger.Discount.Percentage;
        }

        var price = trip.Price * (decimal) ((100 - passengerDiscount) / 100.0) *
                    (stations.Count / (decimal) tripStations.Count);

        var seat = seatsToBook.First();

        var ticket = Ticket.Create(trip, passenger, price, seat, bookTicket.TripDate, stations);

        await _ticketRepository.AddAsync(ticket);
        
        return _mapper.Map<TicketDto>(ticket);
    }

    private static Dictionary<Seat, List<Station>> GetBookedStationsForSeats(Trip trip, DateOnly tripDate)
    {
        Dictionary<Seat, List<Station>> bookedStationsForSeat = new();
        foreach (var seat in trip.Train.Seats)
        {
            var ticketsForSeat = seat.Ticket.Where(x => x.TripDate == tripDate).ToList();
            if (ticketsForSeat.Count == 0)
            {
                bookedStationsForSeat.Add(seat, new List<Station>());
            }
            else
            {
                var bookedStations = ticketsForSeat.SelectMany(x => x.Stations).ToList();
                bookedStations.Reverse();
                if (bookedStations.Count > 0)
                    bookedStations.RemoveAt(bookedStations.Count - 1);
                bookedStationsForSeat.Add(seat, bookedStations);
            }
        }

        return bookedStationsForSeat;
    }

    private static IEnumerable<Seat> GetAvailableSeats(Trip trip, DateOnly tripDate, Station startStation, Station endStation)
    {
        var bookedStationsForSeat = GetBookedStationsForSeats(trip, tripDate);
        var tripStations = trip.Schedules.Select(x => x.Station).ToList();
        var stationsToBook = GetStationsToBook(tripStations, startStation, endStation);

        List<Seat> availableSeats = new();
        foreach (var (seat, stations) in bookedStationsForSeat)
            if (!stations.Any(x => stationsToBook.Contains(x)))
                availableSeats.Add(seat);

        return availableSeats;
    }

    private static IEnumerable<Station> GetStationsToBook(List<Station> stations, Station startStation, Station endStation)
    {
        var startStationIndex = stations.IndexOf(startStation);
        var endStationIndex = stations.IndexOf(endStation);
        var stationsToBook = stations.GetRange(startStationIndex, endStationIndex - startStationIndex);

        return stationsToBook;
    }
}