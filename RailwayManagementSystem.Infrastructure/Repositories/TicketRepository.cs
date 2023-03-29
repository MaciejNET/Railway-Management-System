using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Ticket>> GetByTripIdAsync(int id) 
        => await _context.Tickets
            .Where(x => x.TripId == id)
            .AsNoTracking()
            .ToListAsync();

    public async Task<IEnumerable<Ticket>> GetByPassengerIdAsync(int id) 
        => await _context.Tickets
            .Include(x => x.Passenger)
            .Include(x => x.Seat)
            .Include(x => x.Trip)
            .ThenInclude(x => x.Train)
            .Include(x => x.Stations)
            .Where(x => x.PassengerId == id)
            .AsNoTracking()
            .ToListAsync();
}