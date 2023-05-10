using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.Admin;
using RailwayManagementSystem.Application.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/admins")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAdmin(CreateAdmin createAdmin)
    {
        var adminOrError = await _adminService.CreateAdmin(createAdmin);

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAdmin loginAdmin)
    {
        var adminOrError = await _adminService.LoginAdmin(loginAdmin);

        return NoContent();
    }
}