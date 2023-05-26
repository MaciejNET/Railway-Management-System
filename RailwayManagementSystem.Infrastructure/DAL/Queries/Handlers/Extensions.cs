using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal static class Extensions
{
    public static SeatDto AsDto(this Seat seat)
        => new()
        {
            Id = seat.Id,
            Place = seat.Place,
            SeatNumber = seat.SeatNumber
        };

    public static TicketDto AsDto(this Ticket ticket)
        => new()
        {
            Id = ticket.Id,
            TrainName = ticket.Trip.Train.Name,
            CarrierName = ticket.Trip.Train.Carrier.Name,
            Price = ticket.Price,
            StartStation = ticket.Stations.First().Name,
            EndStation = ticket.Stations.Last().Name,
            TripDate = ticket.TripDate,
            SeatNumber = ticket.Seat.SeatNumber,
            Place = ticket.Seat.Place
        };
}