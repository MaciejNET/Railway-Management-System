using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.RailwayEmployee;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IRailwayEmployeeService
{
    Task<ServiceResponse<RailwayEmployeeDto>> CreateRailwayEmployee(CreateRailwayEmployee createRailwayEmployee);
    Task<ServiceResponse<string>> LoginRailwayEmployee(LoginRailwayEmployee loginRailwayEmployee);
}