using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresStationRepository : IStationRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresStationRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> ExistsByNameAsync(string name)
        => _dbContext.Stations.AnyAsync(x => x.Name == name);


    public Task<Station> GetByNameAsync(string name)
        => _dbContext.Stations.SingleOrDefaultAsync(x => x.Name == name);

    public async Task AddAsync(Station station)
    {
        await _dbContext.Stations.AddAsync(station);
        await _dbContext.SaveChangesAsync();
    }
}