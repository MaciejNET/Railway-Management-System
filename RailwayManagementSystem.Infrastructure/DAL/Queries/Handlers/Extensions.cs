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

    public static PassengerDto AsDto(this Passenger passenger)
        => new()
        {
            FirstName = passenger.FirstName,
            LastName = passenger.LastName,
            Email = passenger.Email,
            DateOfBirth = passenger.DateOfBirth,
            Discount = passenger.Discount is not null ? passenger.Discount.Name : string.Empty
        };

    public static StationScheduleDto AsDto(this StationSchedule stationSchedule)
        => new()
        {
            StationName = stationSchedule.Station.Name,
            ArrivalTime = stationSchedule.ArrivalTime,
            DepartureTime = stationSchedule.DepartureTime,
            Platform = stationSchedule.Platform
        };

    public static TripDto AsDto(this Trip trip)
        => new()
        {
            Id = trip.Id,
            TrainName = trip.Train.Name,
            CarrierName = trip.Train.Carrier.Name,
            Price = trip.Price,
            Stations = trip.Schedule.Stations.Select(x => x.AsDto()).OrderBy(x => x.DepartureTime)
        };

    public static CarrierDto AsDto(this Carrier carrier)
        => new()
        {
            Id = carrier.Id,
            Name = carrier.Name
        };

    public static TrainDto AsDto(this Train train)
        => new()
        {
            Id = train.Id,
            Name = train.Name,
            SeatsAmount = train.SeatsAmount
        };

    public static DiscountDto AsDto(this Discount discount)
        => new()
        {
            Id = discount.Id,
            Name = discount.Name,
            Percentage = discount.Percentage
        };

    public static StationDto AsDto(this Station station)
        => new()
        {
            Id = station.Id,
            Name = station.Name,
            City = station.City,
            NumberOfPlatform = station.NumberOfPlatforms
        };

    public static AdminDto AsDto(this Admin admin)
        => new()
        {
            Id = admin.Id,
            Name = admin.Name
        };
}