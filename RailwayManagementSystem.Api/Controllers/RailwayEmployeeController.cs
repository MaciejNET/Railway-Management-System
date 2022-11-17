using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Admin;
using RailwayManagementSystem.Infrastructure.Commands.RailwayEmployee;
using RailwayManagementSystem.Infrastructure.Services;

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
    public async Task<IActionResult> CreateRailwayEmployee(CreateRailwayEmployee createRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeService.CreateRailwayEmployee(createRailwayEmployee);

        if (railwayEmployee.Success is false)
        {
            return BadRequest(railwayEmployee.Message);
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRailwayEmployee loginRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeService.LoginRailwayEmployee(loginRailwayEmployee);

        if (railwayEmployee.Success is false)
        {
            return BadRequest(railwayEmployee.Message);
        }

        return Ok(railwayEmployee.Data);
    }
}