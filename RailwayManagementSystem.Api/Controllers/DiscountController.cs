using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Commands.Discount;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/discounts")]
public class DiscountController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<DiscountDto>>> Get()
    {
        var query = new GetDiscounts();

        var discounts = await queryDispatcher.QueryAsync(query);

        return Ok(discounts);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Post(CreateDiscount command)
    {
        command = command with {Id = Guid.NewGuid()};

        await commandDispatcher.DispatchAsync(command);

        return CreatedAtAction(nameof(Get), null, null);
    }

    [HttpDelete("{discountId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Delete(Guid discountId)
    {
        var command = new DeleteDiscount(discountId);

        await commandDispatcher.DispatchAsync(command);

        return NoContent();
    }
}