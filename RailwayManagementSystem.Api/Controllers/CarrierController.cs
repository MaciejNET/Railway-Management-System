using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Commands.Carrier;
using RailwayManagementSystem.Application.Commands.Train;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/carriers")]
public class CarrierController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    : ControllerBase
{
    [HttpGet("{carrierId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarrierDto>> Get(Guid carrierId)
    {
        var query = new GetCarrier {Id = carrierId};

        var carrier = await queryDispatcher.QueryAsync(query);

        return Ok(carrier);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<CarrierDto>>> GetAll()
    {
        var query = new GetCarriers();

        var carriers = await queryDispatcher.QueryAsync(query);

        return Ok(carriers);
    }

    [HttpGet("{carrierId:guid}/trains")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetTrains(Guid carrierId)
    {
        var query = new GetCarrierTrains {Id = carrierId};

        var trains = await queryDispatcher.QueryAsync(query);

        return Ok(trains);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Post(CreateCarrier command)
    {
        command = command with {Id = Guid.NewGuid()};

        await commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get), command.Id, null);
    }

    [HttpPost("{carrierId:guid}/trains")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Post(Guid carrierId, CreateTrain command)
    {
        command = command with {Id = Guid.NewGuid(), CarrierId = carrierId};

        await commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(GetTrains), null, null);
    }

    [HttpDelete("{carrierId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Delete(Guid carrierId)
    {
        var command = new DeleteCarrier(carrierId);

        await commandDispatcher.DispatchAsync(command);

        return NoContent();
    }
}