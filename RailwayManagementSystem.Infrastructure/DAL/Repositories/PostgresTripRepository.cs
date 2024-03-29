using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresTripRepository : ITripRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresTripRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Trip?> GetByIdAsync(Guid id)
        => await _dbContext.Trips.FindAsync(id);

    public async Task AddAsync(Trip trip)
    {
        await _dbContext.Trips.AddAsync(trip);
        await _dbContext.Schedules.AddAsync(trip.Schedule);
        await _dbContext.StationSchedules.AddRangeAsync(trip.Schedule.Stations);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Trip trip)
    {
        _dbContext.Trips.Update(trip);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Trip trip)
    {
        _dbContext.Trips.Remove(trip);
        await _dbContext.SaveChangesAsync();
    }
}