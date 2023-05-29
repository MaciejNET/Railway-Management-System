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

    public Task<Carrier> GetByNameAsync(string name)
        => _dbContext.Carriers.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Carrier carrier)
    {
        await _dbContext.Carriers.AddAsync(carrier);
        await _dbContext.SaveChangesAsync();
    }
}