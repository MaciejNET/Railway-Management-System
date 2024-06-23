using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Ticket;

internal sealed class BookTicketHandler(
    ITripRepository tripRepository,
    IPassengerRepository passengerRepository,
    IStationRepository stationRepository,
    ISeatRepository seatRepository)
    : ICommandHandler<BookTicket>
{
    public async Task HandleAsync(BookTicket command)
    {
        var tripId = new TripId(command.TripId);
        var passengerId = new UserId(command.PassengerId);
        var startStationName = new StationName(command.StartStation);
        var endStationName = new StationName(command.EndStation);
        
        var trip = await tripRepository.GetByIdAsync(tripId);

        if (trip is null)
        {
            throw new TripNotFoundException(tripId);
        }

        var passenger = await passengerRepository.GetByIdAsync(passengerId);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(passengerId);
        }

        var startStation = await stationRepository.GetByNameAsync(startStationName);

        if (startStation is null)
        {
            throw new StationNotFoundException(startStationName);
        }
        
        var endStation = await stationRepository.GetByNameAsync(endStationName);

        if (endStation is null)
        {
            throw new StationNotFoundException(endStationName);
        }

        Seat? seat = null;
        
        if (command.SeatId is not null)
        {
            seat = await seatRepository.GetByIdAsync(command.SeatId);

            if (seat is null)
            {
                throw new SeatNotFoundException(command.SeatId);
            }
        }


        trip.ReserveTicket(passenger, startStation, endStation, command.TripDate, seat);
        await tripRepository.UpdateAsync(trip);
    }
}