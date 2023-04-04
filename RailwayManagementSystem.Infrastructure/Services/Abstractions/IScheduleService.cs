using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IScheduleService
{
    Task<ErrorOr<IEnumerable<ScheduleDto>>> GetByTripId(int id);
}