using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class TripRepository : GenericRepository<Trip>, ITripRepository
{
    public TripRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public new async Task<Trip?> GetByIdAsync(int id) 
        => await _context.Trips
            .Include(x => x.Train)
            .ThenInclude(x => x.Seats)
            .ThenInclude(x => x.Ticket)
            .Include(x => x.TripInterval)
            .Include(x => x.Schedules)
            .ThenInclude(x => x.Station)
            .FirstOrDefaultAsync(x => x.Id == id);

    public new async Task<IEnumerable<Trip>> GetAllAsync() 
        => await _context.Trips
            .Include(x => x.Train)
            .ThenInclude(x => x.Seats)
            .ThenInclude(x => x.Ticket)
            .Include(x => x.TripInterval)
            .Include(x => x.Schedules)
            .ThenInclude(x => x.Station)
            .ToListAsync();

    public async Task<IEnumerable<Trip>> GetByTrainIdAsync(int id) 
        => await _context.Trips
            .Include(x => x.Train)
            .ThenInclude(x => x.Seats)
            .ThenInclude(x => x.Ticket)
            .Include(x => x.TripInterval)
            .Include(x => x.Schedules)
            .ThenInclude(x => x.Station)
            .Where(x => x.TrainId == id)
            .ToListAsync();
}