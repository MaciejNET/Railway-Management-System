using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Admin;
using RailwayManagementSystem.Infrastructure.Services;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

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
    public async Task<IActionResult> CreateAdmin([FromBody] CreateAdmin createAdmin)
    {
        var admin = await _adminService.CreateAdmin(createAdmin);

        if (admin.Success is false)
        {
            return BadRequest(admin.Message);
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginAdmin loginAdmin)
    {
        var admin = await _adminService.LoginAdmin(loginAdmin);

        if (admin.Success is false)
        {
            return BadRequest(admin.Message);
        }

        return Ok(admin.Data);
    }
}