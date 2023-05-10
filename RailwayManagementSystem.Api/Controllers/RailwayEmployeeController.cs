using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.RailwayEmployee;
using RailwayManagementSystem.Application.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/railwayEmployees")]
public class RailwayEmployeeController : ControllerBase
{
    private readonly IRailwayEmployeeService _railwayEmployeeService;

    public RailwayEmployeeController(IRailwayEmployeeService railwayEmployeeService)
    {
        _railwayEmployeeService = railwayEmployeeService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateRailwayEmployee([FromBody] CreateRailwayEmployee createRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeService.CreateRailwayEmployee(createRailwayEmployee);

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRailwayEmployee loginRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeService.LoginRailwayEmployee(loginRailwayEmployee);

        return NoContent();
    }
}