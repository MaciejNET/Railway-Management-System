using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Commands.Station;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/stations")]
public class StationController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<StationDto>>> Get()
    {
        var query = new GetStations();

        var stations = await queryDispatcher.QueryAsync(query);

        return Ok(stations);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Post(CreateStation command)
    {
        command = command with {Id = Guid.NewGuid()};

        await commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get), null, null);
    }

    [HttpDelete("{stationId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Delete(Guid stationId)
    {
        var command = new DeleteStation(stationId);

        await commandDispatcher.DispatchAsync(command);

        return NoContent();
    }
}