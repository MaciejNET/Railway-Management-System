using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface IScheduleRepository : IGenericRepository<Schedule>
{
    Task<IEnumerable<Schedule>> GetByTripId(int id);
    Task<IEnumerable<Schedule>> GetByDepartureTimeAndStationId(TimeOnly departureTime, int id);
}