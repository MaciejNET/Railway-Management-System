using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class AdminRepository : GenericRepository<Admin>, IAdminRepository
{
    public AdminRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<Admin?> GetByName(string name)
    {
        return await _context.Admins.FirstOrDefaultAsync(x => x.Name.Value == name);
    }
}