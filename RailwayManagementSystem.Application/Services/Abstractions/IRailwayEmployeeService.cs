using ErrorOr;
using RailwayManagementSystem.Application.Commands.RailwayEmployee;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IRailwayEmployeeService
{
    Task<RailwayEmployeeDto> CreateRailwayEmployee(CreateRailwayEmployee createRailwayEmployee);
    Task<string> LoginRailwayEmployee(LoginRailwayEmployee loginRailwayEmployee);
}