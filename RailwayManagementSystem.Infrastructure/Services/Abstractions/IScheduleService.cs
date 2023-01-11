using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IScheduleService
{
    Task<ServiceResponse<IEnumerable<ScheduleDto>>> GetByTripId(int id);
}