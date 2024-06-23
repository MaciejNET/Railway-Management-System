using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetStationsHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetStations, IEnumerable<StationDto>>
{
    public async Task<IEnumerable<StationDto>> HandleAsync(GetStations query)
    {
        var stations = await dbContext.Stations.AsNoTracking().ToListAsync();

        return stations.Select(x => x.AsDto());
    }
}