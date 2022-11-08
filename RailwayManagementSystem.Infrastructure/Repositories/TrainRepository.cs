using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class TrainRepository : GenericRepository<Train>, ITrainRepository
{
    public TrainRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public new async Task<Train?> GetById(int id)
    {
        return await _context.Trains.Include(x => x.Career).FirstOrDefaultAsync(x => x.Id == id);
    }

    public new async Task<IEnumerable<Train>> GetAll()
    {
        return await _context.Trains.Include(x => x.Career).ToListAsync();
    }

    public async Task<Train?> GetTrainByName(string name)
    {
        return await _context.Trains.FirstOrDefaultAsync(x => x.Name.Value == name);
    }

    public async Task<IEnumerable<Train>> GetByCareerId(int id)
    {
        return await _context.Trains.Include(x => x.Career).Where(x => x.CareerId == id).ToListAsync();
    }

    public async Task<IEnumerable<Train>> GetByCareerName(string name)
    {
        return await _context.Trains.Where(x => x.Career.Name.Value == name).ToListAsync();
    }
}