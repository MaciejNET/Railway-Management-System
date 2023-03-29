using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface IScheduleRepository : IGenericRepository<Schedule>
{
    Task<IEnumerable<Schedule>> GetByTripIdAsync(int id);
    Task<IEnumerable<Schedule>> GetByDepartureTimeAndStationIdAsync(TimeOnly departureTime, int id);
}