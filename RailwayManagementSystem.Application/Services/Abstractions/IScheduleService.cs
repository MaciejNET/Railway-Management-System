using ErrorOr;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDto>> GetByTripId(int id);
}