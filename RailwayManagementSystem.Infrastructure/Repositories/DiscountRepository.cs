using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
{
    public DiscountRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<Discount?> GetByName(string name)
    {
        return await _context.Discounts.FirstOrDefaultAsync(x => x.Name.Value == name);
    }
}