using Humanizer;
using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetAvailableSeatsForTripHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetAvailableSeatsForTrip, IEnumerable<SeatDto>>
{
    public async Task<IEnumerable<SeatDto>> HandleAsync(GetAvailableSeatsForTrip query)
    {
        var tripId = new TripId(query.TripId);
        var startStationName = new StationName(query.StartStation.Replace("_", " "));
        var endStationName = new StationName(query.EndStation.Replace("_", " "));
        
        var trip = await dbContext.Trips
            .Include(x => x.Train)
            .ThenInclude(x => x.Seats)
            .Include(x => x.Schedule)
            .ThenInclude(x => x.Stations)
            .ThenInclude(x => x.Station)
            .SingleOrDefaultAsync(x => x.Id == tripId);

        if (trip is null)
        {
            throw new TripNotFoundException(tripId);
        }

        var startStation = await dbContext.Stations.FirstOrDefaultAsync(x => x.Name == startStationName);

        if (startStation is null)
        {
            throw new StationNotFoundException(query.StartStation);
        }
        
        var endStation = await dbContext.Stations.FirstOrDefaultAsync(x => x.Name == endStationName);

        if (endStation is null)
        {
            throw new StationNotFoundException(query.EndStation);
        }
        
        var availableSeats = trip.GetAvailableSeats(startStation, endStation, query.DepartureTime);

        return availableSeats.Select(x => x.AsDto());
    }
}