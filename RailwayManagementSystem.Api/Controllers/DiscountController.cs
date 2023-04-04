using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Discount;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[Route("api/discounts")]
public class DiscountController : ApiController
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var discountOrError = await _discountService.GetById(id);

        return discountOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var discountsOrError = await _discountService.GetAll();

        return discountsOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddDiscount([FromBody] CreateDiscount createDiscount)
    {
        var discountOrError = await _discountService.AddDiscount(createDiscount);

        return discountOrError.Match(
            value => Created($"api/discounts/{value.Id}", null),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var discountOrError = await _discountService.Delete(id);

        return discountOrError.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }
}