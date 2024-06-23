using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresStationRepository(RailwayManagementSystemDbContext dbContext) : IStationRepository
{
    public Task<bool> ExistsByNameAsync(string name)
        => dbContext.Stations.AnyAsync(x => x.Name == name);

    public async Task<Station?> GetByIdAsync(Guid id)
        => await dbContext.Stations.FindAsync(id);


    public Task<Station?> GetByNameAsync(string name)
        => dbContext.Stations.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Station station)
    {
        await dbContext.Stations.AddAsync(station);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Station station)
    {
        dbContext.Stations.Remove(station);
        await dbContext.SaveChangesAsync();
    }
}