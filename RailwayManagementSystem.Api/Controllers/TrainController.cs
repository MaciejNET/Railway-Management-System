using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Commands.Train;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("train")]
public class TrainController : ControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public TrainController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpDelete("{trainId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Delete(Guid trainId)
    {
        var command = new DeleteTrain(trainId);

        await _commandDispatcher.DispatchAsync(command);

        return NoContent();
    }
}