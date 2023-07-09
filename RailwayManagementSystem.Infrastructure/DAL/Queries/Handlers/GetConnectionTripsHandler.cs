using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetConnectionTripsHandler : IQueryHandler<GetConnectionTrips, IEnumerable<TripDto>>
{
    public Task<IEnumerable<TripDto>> HandleAsync(GetConnectionTrips query)
    {
        throw new NotImplementedException();
    }
}