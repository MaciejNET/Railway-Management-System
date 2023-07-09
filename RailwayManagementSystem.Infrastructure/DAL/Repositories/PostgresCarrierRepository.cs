using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresCarrierRepository : ICarrierRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresCarrierRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsByNameAsync(string name)
        => _dbContext.Carriers.AnyAsync(x => x.Name == name);

    public async Task<Carrier?> GetByIdAsync(Guid id)
        => await _dbContext.Carriers.FindAsync(id);

    public async Task AddAsync(Carrier carrier)
    {
        await _dbContext.Carriers.AddAsync(carrier);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Carrier carrier)
    {
        _dbContext.Carriers.Remove(carrier);
        await _dbContext.SaveChangesAsync();
    }
}