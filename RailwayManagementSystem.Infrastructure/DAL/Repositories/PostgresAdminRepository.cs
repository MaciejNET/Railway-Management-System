using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresAdminRepository : IAdminRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresAdminRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistByNameAsync(string name)
        => _dbContext.Admins.AnyAsync(x => x.Name == name);

    public Task<Admin?> GetByNameAsync(string name)
        => _dbContext.Admins.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Admin admin)
    {
        await _dbContext.AddAsync(admin);
        await _dbContext.SaveChangesAsync();
    }
}