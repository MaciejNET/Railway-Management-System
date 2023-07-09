using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetTripHandler : IQueryHandler<GetTrip, TripDto>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetTripHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TripDto> HandleAsync(GetTrip query)
    {
        var tripId = new TripId(query.Id);

        var trip = await _dbContext.Trips
            .Include(x => x.Train)
            .ThenInclude(x => x.Carrier)
            .Include(x => x.Schedule)
            .ThenInclude(x => x.Stations)
            .ThenInclude(x => x.Station)
            .SingleOrDefaultAsync(x => x.Id == tripId);

        if (trip is null)
        {
            throw new TripNotFoundException(tripId);
        }

        return trip.AsDto();
    }
}