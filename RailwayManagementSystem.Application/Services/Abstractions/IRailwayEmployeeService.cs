using ErrorOr;
using RailwayManagementSystem.Application.Commands.RailwayEmployee;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IRailwayEmployeeService
{
    Task<ErrorOr<RailwayEmployeeDto>> CreateRailwayEmployee(CreateRailwayEmployee createRailwayEmployee);
    Task<ErrorOr<string>> LoginRailwayEmployee(LoginRailwayEmployee loginRailwayEmployee);
}