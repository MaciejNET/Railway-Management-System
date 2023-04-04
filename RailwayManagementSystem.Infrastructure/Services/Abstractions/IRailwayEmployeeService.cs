using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.RailwayEmployee;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IRailwayEmployeeService
{
    Task<ErrorOr<RailwayEmployeeDto>> CreateRailwayEmployee(CreateRailwayEmployee createRailwayEmployee);
    Task<ErrorOr<string>> LoginRailwayEmployee(LoginRailwayEmployee loginRailwayEmployee);
}