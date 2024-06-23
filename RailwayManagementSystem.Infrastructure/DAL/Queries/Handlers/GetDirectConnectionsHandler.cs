using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Application.Responses;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetDirectConnectionsHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetDirectConnections, IEnumerable<Connection>>
{
    public async Task<IEnumerable<Connection>> HandleAsync(GetDirectConnections query)
    {
        var startStationName = new StationName(query.StartStation);
        var endStationName = new StationName(query.EndStation);

        var startStation = await dbContext.Stations.FirstOrDefaultAsync(x => x.Name == startStationName);
        var endStation = await dbContext.Stations.FirstOrDefaultAsync(x => x.Name == endStationName);
        
        if (startStation is null)
        {
            throw new StationNotFoundException(startStationName);
        }
        
        if (endStation is null)
        {
            throw new StationNotFoundException(endStationName);
        }
        
        var schedules = await dbContext.Schedules
            .Include(x => x.Stations)
            .ThenInclude(stationSchedule => stationSchedule.Station)
            .Where(x => x.ValidDate.From <= DateOnly.FromDateTime(query.DepartureTime.Date) &&
                        x.ValidDate.To >= DateOnly.FromDateTime(query.DepartureTime.Date))
            .ToListAsync();

        List<Connection> directConnections = [];

        foreach (var schedule in schedules)
        {
            var startStationSchedule = schedule.Stations.FirstOrDefault(x => x.Station == startStation);
            var endStationSchedule = schedule.Stations.FirstOrDefault(x => x.Station == endStation);

            if (startStationSchedule is not null && endStationSchedule is not null &&
                startStationSchedule.DepartureTime >= TimeOnly.FromTimeSpan(query.DepartureTime.TimeOfDay) &&
                endStationSchedule.ArrivalTime > TimeOnly.FromTimeSpan(query.DepartureTime.TimeOfDay))
            {
                var trip = await dbContext.Trips.FirstOrDefaultAsync(x => x.Id == schedule.TripId);

                if (trip is not null &&
                    trip.Schedule.TripAvailability.IsTripRunningOnGivenDate(query.DepartureTime.DayOfWeek))
                {
                    try
                    {
                        var connectionTripStationsCount =
                            trip.GetTripStationsCount(startStationSchedule, endStationSchedule);
                        var connectionTripPrice =
                            trip.CalculateConnectionTripPrice(startStationSchedule, endStationSchedule);

                        var connectionTrip = new ConnectionTrip
                        {
                            TripId = trip.Id,
                            StartStation = startStationSchedule.Station.Name,
                            EndStation = endStationSchedule.Station.Name,
                            DepartureTime = startStationSchedule.DepartureTime,
                            ArrivalTime = endStationSchedule.ArrivalTime,
                            Price = connectionTripPrice,
                            TravelTime = TimeOnly.FromTimeSpan(endStationSchedule.ArrivalTime - startStationSchedule.DepartureTime)
                        };

                        var connection = new Connection();
                        connection.Trips.Add(connectionTrip);
                        connection.NoChanges = connection.Trips.Count - 1;
                        connection.TotalTravelTime = TimeOnly.FromTimeSpan(connectionTrip.TravelTime.ToTimeSpan());
                        connection.TotalPrice += connectionTrip.Price;

                        directConnections.Add(connection);
                    }
                    catch (InvalidStationOrderException)
                    {
                        continue;
                    }
                }
            }
        }

        return directConnections;
    }
}