using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresAdminRepository(RailwayManagementSystemDbContext dbContext) : IAdminRepository
{
    public Task<bool> ExistByNameAsync(string name)
        => dbContext.Admins.AnyAsync(x => x.Name == name);

    public async Task<Admin?> GetByIdAsync(Guid id)
        => await dbContext.Admins.FindAsync(id);

    public Task<Admin?> GetByNameAsync(string name)
        => dbContext.Admins.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Admin admin)
    {
        await dbContext.AddAsync(admin);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Admin admin)
    {
        dbContext.Admins.Remove(admin);
        await dbContext.SaveChangesAsync();
    }
}