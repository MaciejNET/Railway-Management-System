using AutoMapper;
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

    public async Task<ServiceResponse<TicketDto>> BookTicket(BookTicket bookTicket, int passengerId)
    {
        var trip = await _tripRepository.GetByIdAsync(bookTicket.TripId);

        if (trip is null)
        {
            var serviceResponse = new ServiceResponse<TicketDto>
            {
                Success = false,
                Message = "Trip not found."
            };
            return serviceResponse;
        }

        if (bookTicket.TripDate < DateOnly.FromDateTime(DateTime.Now))
        {
            var serviceResponse = new ServiceResponse<TicketDto>
            {
                Success = false,
                Message = "Cannot book ticket for a past date."
            };
            return serviceResponse;
        }
        
        if (TripExtensions.IsTrainRunsOnGivenDate(trip, bookTicket.TripDate) is false)
        {
            var serviceResponse = new ServiceResponse<TicketDto>
            {
                Success = false,
                Message = "This trip does not run on given date."
            };
            return serviceResponse;
        }

        var passenger = await _passengerRepository.GetByIdAsync(passengerId);

        if (passenger is null)
        {
            var serviceResponse = new ServiceResponse<TicketDto>
            {
                Success = false,
                Message = "Passenger not found."
            };
            return serviceResponse;
        }

        Station startStation, endStation;
        try
        {
            startStation = await GetOrFailStation(bookTicket.StartStation);
            endStation = await GetOrFailStation(bookTicket.EndStation);
        }
        catch (Exception ex)
        {
            var serviceResponse = new ServiceResponse<TicketDto>()
            {
                Success = false,
                Message = ex.Message
            };

            return serviceResponse;
        }

        var tripStations = trip.Schedules.Select(x => x.Station).ToList();
        if ((tripStations.Contains(startStation) && tripStations.Contains(endStation)) is false)
        {
            var serviceResponse = new ServiceResponse<TicketDto>
            {
                Success = false,
                Message = "Trip schedule does not contains provided stations"
            };
            return serviceResponse;
        }

        var seatsToBook = GetAvailableSeats(trip, bookTicket.TripDate, startStation, endStation).ToList();

        if (seatsToBook.Count == 0)
        {
            var serviceResponse = new ServiceResponse<TicketDto>
            {
                Success = false,
                Message = "There is no free seat to book"
            };
            return serviceResponse;
        }

        var stations = GetStationsToBook(tripStations, startStation, endStation).ToList();
        stations.Add(endStation);
        
        var passengerDiscount = 0;
        if (passenger.Discount is not null)
        {
            passengerDiscount = passenger.Discount.Percentage;
        }
        
        var ticket = new Ticket
        {
            Trip = trip,
            Passenger = passenger,
            Price = trip.Price * (decimal) ((100 - passengerDiscount) / 100.0) * 
                    (stations.Count / (decimal)tripStations.Count),
            Seat = seatsToBook.First(),
            TripDate = bookTicket.TripDate,
            Stations = stations
        };

        await _ticketRepository.AddAsync(ticket);
        await _ticketRepository.SaveChangesAsync();
        
        var response = new ServiceResponse<TicketDto>
        {
            Data = _mapper.Map<TicketDto>(ticket)
        };
        return response;
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

    private async Task<Station> GetOrFailStation(string stationName)
    {
        var station = await _stationRepository.GetByNameAsync(stationName);
        if (station is null) throw new Exception($"Station with name: '{stationName}' does not exist");

        return station;
    }
}