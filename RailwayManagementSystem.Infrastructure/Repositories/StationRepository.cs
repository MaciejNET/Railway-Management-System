using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class StationRepository : GenericRepository<Station>, IStationRepository
{
    public StationRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<Station?> GetByName(string name)
    {
        return await _context.Stations.FirstOrDefaultAsync(x => x.Name.Value == name);
    }

    public async Task<IEnumerable<Station>> GetByCity(string city)
    {
        return await _context.Stations.Where(x => x.City.Value == city).ToListAsync();
    }
}