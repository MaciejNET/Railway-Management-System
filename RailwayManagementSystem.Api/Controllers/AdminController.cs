using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Admin;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[Route("api/admins")]
public class AdminController : ApiController
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAdmin([FromBody] CreateAdmin createAdmin)
    {
        var adminOrError = await _adminService.CreateAdmin(createAdmin);

         return adminOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAdmin loginAdmin)
    {
        var adminOrError = await _adminService.LoginAdmin(loginAdmin);

        return adminOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }
}