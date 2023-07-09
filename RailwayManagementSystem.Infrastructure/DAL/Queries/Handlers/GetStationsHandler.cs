using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetStationsHandler : IQueryHandler<GetStations, IEnumerable<StationDto>>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetStationsHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<StationDto>> HandleAsync(GetStations query)
    {
        var stations = await _dbContext.Stations.AsNoTracking().ToListAsync();

        return stations.Select(x => x.AsDto());
    }
}