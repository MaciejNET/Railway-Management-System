using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresStationScheduleRepository(RailwayManagementSystemDbContext dbContext)
    : IStationScheduleRepository
{
    public Task<bool> IsAnyScheduleInStation(Station station)
        => dbContext.StationSchedules.AnyAsync(x => x.Station == station);
}