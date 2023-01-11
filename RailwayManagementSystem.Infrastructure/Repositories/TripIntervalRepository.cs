using System.Data.Entity;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class TripIntervalRepository : GenericRepository<TripInterval>, ITripIntervalRepository
{
    public TripIntervalRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<TripInterval> GetByTrip(Trip trip) 
        => await _context.TripIntervals.FirstOrDefaultAsync(x => x.Trip.Equals(trip));
}