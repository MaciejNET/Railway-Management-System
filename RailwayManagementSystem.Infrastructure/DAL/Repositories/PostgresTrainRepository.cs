using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresTrainRepository(RailwayManagementSystemDbContext dbContext) : ITrainRepository
{
    public Task<bool> ExistsByNameAsync(string name)
        => dbContext.Trains.AnyAsync(x => x.Name == name);

    public Task<bool> IsTrainInUse(Train train)
        => dbContext.Trips.AnyAsync(x => x.Train == train);

    public async Task<Train?> GetByIdAsync(Guid id)
        => await dbContext.Trains.FindAsync(id);

    public Task<Train?> GetByNameAsync(string name)
        => dbContext.Trains.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Train train)
    {
        await dbContext.Trains.AddAsync(train);
        await dbContext.Seats.AddRangeAsync(train.Seats);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Train train)
    {
        dbContext.Trains.Remove(train);
        await dbContext.SaveChangesAsync();
    }
}