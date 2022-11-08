using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class CareerRepository : GenericRepository<Career>, ICareerRepository
{
    public CareerRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<Career?> GetByName(string name)
    {
        return await _context.Careers.FirstOrDefaultAsync(x => x.Name.Value == name);
    }
}