using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
{
    public DiscountRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<Discount?> GetByNameAsync(string name)
        => await _context.Discounts
            .FirstOrDefaultAsync(x => x.Name == name);
}