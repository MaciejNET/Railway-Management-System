using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class ScheduleRepository : GenericRepository<Schedule>, IScheduleRepository
{
    public ScheduleRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Schedule>> GetByTripId(int id)
    {
        return await _context.Schedules.Include(x => x.Station).Where(x => x.TripId == id).ToListAsync();
    }

    public async Task<IEnumerable<Schedule>> GetByDepartureTimeAndStationId(TimeOnly departureTime, int id) 
        => await _context.Schedules.Include(x => x.Station).Include(x => x.Trip)
            .Where(x => x.DepartureTime >= departureTime && x.StationId == id).ToListAsync();
}