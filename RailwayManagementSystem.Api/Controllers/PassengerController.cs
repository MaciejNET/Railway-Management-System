using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Security;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("passenger")]
public class PassengerController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ITokenStorage _tokenStorage;

    public PassengerController(ICommandDispatcher commandDispatcher, ITokenStorage tokenStorage)
    {
        _commandDispatcher = commandDispatcher;
        _tokenStorage = tokenStorage;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Post(RegisterPassenger command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _commandDispatcher.DispatchAsync(command);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<JwtDto>> Post(LoginPassenger command)
    {
        await _commandDispatcher.DispatchAsync(command);
        var jwt = _tokenStorage.Get();
        return jwt;
    }
}