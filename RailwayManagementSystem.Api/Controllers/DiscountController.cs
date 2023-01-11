using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Discount;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/discounts")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var discount = await _discountService.GetById(id);

        if (discount.Success is false)
        {
            return NotFound(discount.Message);
        }

        return Ok(discount.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var discounts = await _discountService.GetAll();

        if (discounts.Success is false)
        {
            return NotFound(discounts.Message);
        }

        return Ok(discounts.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddDiscount([FromBody] CreateDiscount createDiscount)
    {
        var discount = await _discountService.AddDiscount(createDiscount);

        if (discount.Success is false)
        {
            return BadRequest(discount.Message);
        }
        
        if (discount.Data is null)
        {
            return StatusCode(500);
        }

        return Created($"api/discounts/{discount.Data.Id}", null);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var discount = await _discountService.Delete(id);

        if (discount.Success is false)
        {
            return BadRequest(discount.Message);
        }

        return NoContent();
    }
}