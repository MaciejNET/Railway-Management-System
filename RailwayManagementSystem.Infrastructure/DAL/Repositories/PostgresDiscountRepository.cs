using System.Data.Entity;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresDiscountRepository : IDiscountRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresDiscountRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsByNameAsync(string name)
        => _dbContext.Discounts.AnyAsync(x => x.Name == name);

    public async Task AddAsync(Discount discount)
    {
        await _dbContext.Discounts.AddAsync(discount);
        await _dbContext.SaveChangesAsync();
    }
}