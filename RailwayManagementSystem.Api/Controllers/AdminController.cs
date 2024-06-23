using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Commands.Admin;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Application.Security;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("admins")]
public class AdminController(
    IQueryDispatcher queryDispatcher,
    ICommandDispatcher commandDispatcher,
    ITokenStorage tokenStorage)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<IEnumerable<AdminDto>>> Get()
    {
        var query = new GetAdmins();

        var admins = await queryDispatcher.QueryAsync(query);

        return Ok(admins);
    }

    [HttpPost("add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Post(CreateAdmin command)
    {
        command = command with {Id = Guid.NewGuid()};

        await commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get), null, null);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post(LoginAdmin command)
    {
        await commandDispatcher.DispatchAsync(command);
        
        var jwt = tokenStorage.Get();
        
        return jwt;
    }

    [HttpDelete("{adminId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Delete(Guid adminId)
    {
        var command = new DeleteAdmin(adminId);

        await commandDispatcher.DispatchAsync(command);

        return NoContent();
    }
}