using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresPassengerRepository : IPassengerRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresPassengerRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Passenger?> GetByIdAsync(Guid id)
        => await _dbContext.Passengers.FindAsync(id);

    public async Task AddAsync(Passenger passenger)
    {
        await _dbContext.Passengers.AddAsync(passenger);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Passenger passenger)
    {
        _dbContext.Passengers.Update(passenger);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Passenger passenger)
    {
        _dbContext.Passengers.Remove(passenger);
        await _dbContext.SaveChangesAsync();
    }
}