using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Commands.Ticket;
using RailwayManagementSystem.Application.Commands.Trip;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("trips")]
public class TripController : ControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly ICommandDispatcher _commandDispatcher;

    public TripController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    {
        _queryDispatcher = queryDispatcher;
        _commandDispatcher = commandDispatcher;
    }

    [HttpGet("{tripId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TripDto>> Get(Guid tripId)
    {
        var query = new GetTrip {Id = tripId};

        var trip = await _queryDispatcher.QueryAsync(query);

        return Ok(trip);
    }
    
    //TODO: Get Connection

    [HttpGet("get-available-seats")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<SeatDto>>> Get([FromQuery] GetAvailableSeatsForTrip query)
    {
        var seats = await _queryDispatcher.QueryAsync(query);

        return Ok(seats);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Post(CreateTrip command)
    {
        command = command with {Id = Guid.NewGuid()};

        await _commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get), command.Id, null);
    }

    [HttpPost("{id:guid}/book")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "passenger")]
    public async Task<ActionResult> Post(Guid id, BookTicket command)
    {
        var passengerId = Guid.Parse(HttpContext.User.Identity.Name);
        
        command = command with {TripId = id, PassengerId = passengerId};

        await _commandDispatcher.DispatchAsync(command);

        return Ok();
    }

    [HttpDelete("{tripId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Delete(Guid tripId)
    {
        var command = new DeleteTrip(tripId);

        await _commandDispatcher.DispatchAsync(command);

        return NoContent();
    }
}