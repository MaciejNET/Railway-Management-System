using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresDiscountRepository(RailwayManagementSystemDbContext dbContext) : IDiscountRepository
{
    public Task<bool> ExistsByNameAsync(string name)
        => dbContext.Discounts.AnyAsync(x => x.Name == name);

    public async Task<Discount?> GetByIdAsync(Guid id)
        => await dbContext.Discounts.FindAsync(id);

    public Task<Discount?> GetByNameAsync(string name)
        => dbContext.Discounts.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Discount discount)
    {
        await dbContext.Discounts.AddAsync(discount);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Discount discount)
    {
        dbContext.Discounts.Remove(discount);
        await dbContext.SaveChangesAsync();
    }
}