using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.Commands.Ticket;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Application.Security;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/passengers")]
public class PassengerController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly ITokenStorage _tokenStorage;

    public PassengerController(ICommandDispatcher commandDispatcher, ITokenStorage tokenStorage, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _tokenStorage = tokenStorage;
        _queryDispatcher = queryDispatcher;
    }

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult<PassengerDto>> Get()
    {
        var passengerId = Guid.Parse(HttpContext.User.Identity.Name);

        var query = new GetPassenger {Id = passengerId};

        var passenger = await _queryDispatcher.QueryAsync(query);

        return Ok(passenger);
    }

    [HttpGet("{passengerId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize]
    public async Task<ActionResult<PassengerDto>> Get(Guid passengerId)
    {
        var query = new GetPassenger {Id = passengerId};

        var passenger = await _queryDispatcher.QueryAsync(query);

        return Ok(passenger);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> GetAll()
    {
        var query = new GetPassengers();

        var passengers = await _queryDispatcher.QueryAsync(query);

        return Ok(passengers);
    }

    [HttpGet("tickets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetPassengerTickets()
    {
        var passengerId = Guid.Parse(HttpContext.User.Identity.Name);

        var query = new GetPassengersTickets {PassengerId = passengerId};

        var tickets = await _queryDispatcher.QueryAsync(query);
        
        return Ok(tickets);
    }

    [HttpGet("tickets/{ticketId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> GetTicket(Guid ticketId)
    {
        var query = new GetTicket {TicketId = ticketId};

        var ticket = await _queryDispatcher.QueryAsync(query);

        return Ok(ticket);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(RegisterPassenger command)
    {
        command = command with {Id = Guid.NewGuid()};
        await _commandDispatcher.DispatchAsync(command);
        return CreatedAtAction(nameof(Get), command.Id, null);
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post(LoginPassenger command)
    {
        await _commandDispatcher.DispatchAsync(command);
        var jwt = _tokenStorage.Get();
        return jwt;
    }

    [HttpPatch("update-email")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "passenger")]
    public async Task<ActionResult> Patch(UpdateEmail command)
    {
        var passengerId = Guid.Parse(HttpContext.User.Identity.Name);

        command = command with {Id = passengerId};

        await _commandDispatcher.DispatchAsync(command);

        return NoContent();
    }

    [HttpPatch("update-discount")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "passenger")]
    public async Task<ActionResult> Patch(UpdatePassengerDiscount command)
    {
        var passengerId = Guid.Parse(HttpContext.User.Identity.Name);

        command = command with {PassengerId = passengerId};

        await _commandDispatcher.DispatchAsync(command);

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "passenger")]
    public async Task<ActionResult> Delete()
    {
        var passengerId = Guid.Parse(HttpContext.User.Identity.Name);

        var command = new RemovePassenger(Id: passengerId);

        await _commandDispatcher.DispatchAsync(command);

        return NoContent();
    }

    [HttpDelete("tickets/{ticketId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "passenger")]
    public async Task<ActionResult> Delete(Guid ticketId)
    {
        var passengerId = Guid.Parse(HttpContext.User.Identity.Name);

        var command = new CancelTicket(ticketId, passengerId);

        await _commandDispatcher.DispatchAsync(command);

        return NoContent();
    }
}