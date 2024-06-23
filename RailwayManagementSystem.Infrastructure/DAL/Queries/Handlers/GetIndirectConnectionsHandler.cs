using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Application.Responses;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetIndirectConnectionsHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetIndirectConnections, IEnumerable<Connection>>
{
    public async Task<IEnumerable<Connection>> HandleAsync(GetIndirectConnections query)
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
        
        var visited = new HashSet<Station>();
        var queue = new Queue<(Station Station, Connection Connection)>();
        List<Connection> connections = [];
        
        queue.Enqueue((startStation, new Connection()));

        var foundDestination = false;
        
        while (queue.Count > 0 && !foundDestination)
        {
            var (currentStation, connection) = queue.Dequeue();
            visited.Add(currentStation);
            
            var schedules = await dbContext.Schedules
                .Include(x => x.Stations)
                .ThenInclude(x => x.Station)
                .Where(x => x.ValidDate.From <= DateOnly.FromDateTime(query.DepartureTime.Date) &&
                            x.ValidDate.To >= DateOnly.FromDateTime(query.DepartureTime.Date))
                .ToListAsync();

            foreach (var schedule in schedules)
            {
                var startStationSchedule = schedule.Stations.FirstOrDefault(x => x.Station == currentStation);
                if (startStationSchedule is null)
                {
                    continue;
                }

                var nextStations = schedule.Stations
                    .Where(x => x.DepartureTime >= TimeOnly.FromTimeSpan(query.DepartureTime.TimeOfDay) &&
                                x.ArrivalTime > TimeOnly.FromTimeSpan(query.DepartureTime.TimeOfDay) &&
                                !visited.Contains(x.Station))
                    .ToList();

                foreach (var nextStationSchedule in nextStations)
                {
                    var trip = await dbContext.Trips.FirstOrDefaultAsync(x => x.Id == schedule.TripId);

                    if (trip is not null &&
                        trip.Schedule.TripAvailability.IsTripRunningOnGivenDate(query.DepartureTime.DayOfWeek))
                    {
                        try
                        {
                            var connectionTripStationsCount = trip.GetTripStationsCount(startStationSchedule, nextStationSchedule);
                            var connectionTripPrice = trip.CalculateConnectionTripPrice(startStationSchedule, nextStationSchedule);

                            var connectionTrip = new ConnectionTrip
                            {
                                TripId = trip.Id,
                                StartStation = startStationSchedule.Station.Name,
                                EndStation = nextStationSchedule.Station.Name,
                                DepartureTime = startStationSchedule.DepartureTime,
                                ArrivalTime = nextStationSchedule.ArrivalTime,
                                Price = connectionTripPrice,
                                TravelTime = TimeOnly.FromTimeSpan(nextStationSchedule.ArrivalTime - startStationSchedule.DepartureTime)
                            };

                            var newConnection = new Connection(connection);
                            newConnection.Trips.Add(connectionTrip);
                            newConnection.NoChanges = newConnection.Trips.Count - 1;
                            newConnection.TotalTravelTime = TimeOnly.FromTimeSpan(newConnection.TotalTravelTime.ToTimeSpan() + connectionTrip.TravelTime.ToTimeSpan());
                            newConnection.TotalPrice += connectionTrip.Price;
                            
                            if (nextStationSchedule.Station == endStation)
                            {
                                foundDestination = true;
                                connections.Add(newConnection);
                                break;
                            }
                            else
                            {
                                queue.Enqueue((nextStationSchedule.Station, newConnection));
                            }
                        }
                        catch (InvalidStationOrderException)
                        {
                            continue;
                        }
                    }
                }
            }
        }
        
        
        return connections;
    }
}