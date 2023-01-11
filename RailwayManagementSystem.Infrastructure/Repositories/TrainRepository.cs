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
        => await _context.Trains.Include(x => x.Carrier).FirstOrDefaultAsync(x => x.Id == id);

    public new async Task<IEnumerable<Train>> GetAll() 
        => await _context.Trains.Include(x => x.Carrier).AsNoTracking().ToListAsync();

    public async Task<Train?> GetTrainByName(string name) 
        => await _context.Trains.FirstOrDefaultAsync(x => x.Name.Value == name);

    public async Task<IEnumerable<Train>> GetByCarrierId(int id) 
        => await _context.Trains.Include(x => x.Carrier).Where(x => x.CarrierId == id).AsNoTracking().ToListAsync();
}