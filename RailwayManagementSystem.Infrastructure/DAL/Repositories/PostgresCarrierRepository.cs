using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresCarrierRepository(RailwayManagementSystemDbContext dbContext) : ICarrierRepository
{
    public Task<bool> ExistsByNameAsync(string name)
        => dbContext.Carriers.AnyAsync(x => x.Name == name);

    public async Task<Carrier?> GetByIdAsync(Guid id)
        => await dbContext.Carriers.FindAsync(id);

    public async Task AddAsync(Carrier carrier)
    {
        await dbContext.Carriers.AddAsync(carrier);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Carrier carrier)
    {
        dbContext.Carriers.Remove(carrier);
        await dbContext.SaveChangesAsync();
    }
}