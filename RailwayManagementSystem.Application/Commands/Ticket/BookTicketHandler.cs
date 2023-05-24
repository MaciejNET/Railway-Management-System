using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Ticket;

public class BookTicketHandler : ICommandHandler<BookTicket>
{
    private readonly ITripRepository _tripRepository;
    private readonly IPassengerRepository _passengerRepository;
    private readonly IStationRepository _stationRepository;
    private readonly ISeatRepository _seatRepository;

    public BookTicketHandler(ITripRepository tripRepository, IPassengerRepository passengerRepository, IStationRepository stationRepository, ISeatRepository seatRepository)
    {
        _tripRepository = tripRepository;
        _passengerRepository = passengerRepository;
        _stationRepository = stationRepository;
        _seatRepository = seatRepository;
    }

    public async Task HandleAsync(BookTicket command)
    {
        var trip = await _tripRepository.GetByIdAsync(command.TripId);

        if (trip is null)
        {
            throw new TripNotFoundException(command.TripId);
        }

        var passenger = await _passengerRepository.GetByIdAsync(command.PassengerId);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(command.PassengerId);
        }

        var startStation = await _stationRepository.GetByNameAsync(command.StartStation);

        if (startStation is null)
        {
            throw new StationNotFoundException(command.StartStation);
        }
        
        var endStation = await _stationRepository.GetByNameAsync(command.EndStation);

        if (endStation is null)
        {
            throw new StationNotFoundException(command.EndStation);
        }

        Seat? seat = null;
        
        if (command.SeatId is not null)
        {
            seat = await _seatRepository.GetByIdAsync(command.SeatId);

            if (seat is null)
            {
                throw new SeatNotFoundException(command.SeatId);
            }
        }


        trip.ReserveTicket(passenger, startStation, endStation, command.TripDate, seat);
        await _tripRepository.UpdateAsync(trip);
    }
}