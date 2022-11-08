using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class TripRepository : GenericRepository<Trip>, ITripRepository
{
    public TripRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public new async Task<Trip?> GetById(int id)
    {
        return await _context.Trips.Include(x => x.Train).ThenInclude(x => x.Seats).ThenInclude(x => x.Ticket)
            .Include(x => x.TripInterval).Include(x => x.Schedules)
            .ThenInclude(x => x.Station).FirstOrDefaultAsync(x => x.Id == id);
    }

    public new async Task<IEnumerable<Trip>> GetAll()
    {
        return await _context.Trips.Include(x => x.Train).ThenInclude(x => x.Seats).ThenInclude(x => x.Ticket)
            .Include(x => x.TripInterval).Include(x => x.Schedules)
            .ThenInclude(x => x.Station).ToListAsync();
    }

    public async Task<IEnumerable<Trip>> GetByTrainId(int id)
    {
        return await _context.Trips.Include(x => x.Train).ThenInclude(x => x.Seats).ThenInclude(x => x.Ticket)
            .Include(x => x.TripInterval).Include(x => x.Schedules)
            .ThenInclude(x => x.Station).Where(x => x.TrainId == id).ToListAsync();
    }
}