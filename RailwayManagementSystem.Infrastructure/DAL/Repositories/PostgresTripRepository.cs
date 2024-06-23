using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresTripRepository(RailwayManagementSystemDbContext dbContext) : ITripRepository
{
    public async Task<Trip?> GetByIdAsync(Guid id)
        => await dbContext.Trips.FindAsync(id);

    public async Task AddAsync(Trip trip)
    {
        await dbContext.Trips.AddAsync(trip);
        await dbContext.Schedules.AddAsync(trip.Schedule);
        await dbContext.StationSchedules.AddRangeAsync(trip.Schedule.Stations);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Trip trip)
    {
        dbContext.Trips.Update(trip);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Trip trip)
    {
        dbContext.Trips.Remove(trip);
        await dbContext.SaveChangesAsync();
    }
}