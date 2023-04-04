using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.RailwayEmployee;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[Route("api/railwayEmployees")]
public class RailwayEmployeeController : ApiController
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
        var railwayEmployeeOrError = await _railwayEmployeeService.CreateRailwayEmployee(createRailwayEmployee);

        return railwayEmployeeOrError.Match(
            _ => Ok(),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRailwayEmployee loginRailwayEmployee)
    {
        var railwayEmployeeOrError = await _railwayEmployeeService.LoginRailwayEmployee(loginRailwayEmployee);

        return railwayEmployeeOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }
}