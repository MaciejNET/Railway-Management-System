using System.Data.Entity;
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

    public Task<Train> GetByNameAsync(string name)
        => _dbContext.Trains.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Train train)
    {
        await _dbContext.Trains.AddAsync(train);
        await _dbContext.Seats.AddRangeAsync(train.Seats);
        await _dbContext.SaveChangesAsync();
    }
}