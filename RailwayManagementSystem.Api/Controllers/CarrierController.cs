using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.Carrier;
using RailwayManagementSystem.Application.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/carriers")]
public class CarrierController : ControllerBase
{
    private readonly ICarrierService _carrierService;
    private readonly ITrainService _trainService;

    public CarrierController(ICarrierService carrierService, ITrainService trainService)
    {
        _carrierService = carrierService;
        _trainService = trainService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var carrier = await _carrierService.GetById(id);

        return Ok(carrier);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var carrier = await _carrierService.GetByName(name);

        return Ok(carrier);
    }

    [HttpGet("{id:int}/trains")]
    public async Task<IActionResult> GetAllCarrierTrains([FromRoute] int id)
    {
        var trains = await _trainService.GetByCarrierId(id);

        return Ok(trains);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var carrier = await _carrierService.GetAll();

        return Ok(carrier);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCarrier([FromBody] CreateCarrier createCarrier)
    {
        await _carrierService.AddCarrier(createCarrier);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _carrierService.Delete(id);

        return NoContent();
    }
}