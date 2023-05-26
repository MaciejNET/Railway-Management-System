using Humanizer;
using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetAvailableSeatsForTripHandler : IQueryHandler<GetAvailableSeatsForTrip, IEnumerable<SeatDto>>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetAvailableSeatsForTripHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<SeatDto>> HandleAsync(GetAvailableSeatsForTrip query)
    {
        query.StartStation = query.StartStation.Underscore().Replace("_", " ");
        query.EndStation = query.EndStation.Underscore().Replace("_", " ");
        
        var trip = await _dbContext.Trips.FindAsync(query.TripId);

        if (trip is null)
        {
            throw new TripNotFoundException(query.TripId);
        }

        var startStation = await _dbContext.Stations.FirstOrDefaultAsync(x => x.Name.Value.Underscore() == query.StartStation);

        if (startStation is null)
        {
            throw new StationNotFoundException(query.StartStation);
        }
        
        var endStation = await _dbContext.Stations.FirstOrDefaultAsync(x => x.Name.Value.Underscore() == query.EndStation);

        if (endStation is null)
        {
            throw new StationNotFoundException(query.EndStation);
        }
        
        var availableSeats = trip.GetAvailableSeats(startStation, endStation, query.DepartureTime);

        return availableSeats.Select(x => x.AsDto());
    }
}