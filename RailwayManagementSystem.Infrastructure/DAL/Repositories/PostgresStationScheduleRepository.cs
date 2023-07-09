using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresStationScheduleRepository : IStationScheduleRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresStationScheduleRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> IsAnyScheduleInStation(Station station)
        => _dbContext.StationSchedules.AnyAsync(x => x.Station == station);
}