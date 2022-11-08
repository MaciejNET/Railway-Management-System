using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface IScheduleService
{
    Task<ServiceResponse<IEnumerable<ScheduleDto>>> GetByTripId(int id);
}