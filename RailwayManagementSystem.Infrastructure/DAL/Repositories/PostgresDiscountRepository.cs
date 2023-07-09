using Microsoft.EntityFrameworkCore;
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

    public async Task<Discount?> GetByIdAsync(Guid id)
        => await _dbContext.Discounts.FindAsync(id);

    public Task<Discount?> GetByNameAsync(string name)
        => _dbContext.Discounts.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Discount discount)
    {
        await _dbContext.Discounts.AddAsync(discount);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Discount discount)
    {
        _dbContext.Discounts.Remove(discount);
        await _dbContext.SaveChangesAsync();
    }
}