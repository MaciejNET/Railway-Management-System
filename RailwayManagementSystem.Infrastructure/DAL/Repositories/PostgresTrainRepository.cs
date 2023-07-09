using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresTrainRepository : ITrainRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresTrainRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsByNameAsync(string name)
        => _dbContext.Trains.AnyAsync(x => x.Name == name);

    public Task<bool> IsTrainInUse(Train train)
        => _dbContext.Trips.AnyAsync(x => x.Train == train);

    public async Task<Train?> GetByIdAsync(Guid id)
        => await _dbContext.Trains.FindAsync(id);

    public Task<Train?> GetByNameAsync(string name)
        => _dbContext.Trains.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Train train)
    {
        await _dbContext.Trains.AddAsync(train);
        await _dbContext.Seats.AddRangeAsync(train.Seats);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Train train)
    {
        _dbContext.Trains.Remove(train);
        await _dbContext.SaveChangesAsync();
    }
}