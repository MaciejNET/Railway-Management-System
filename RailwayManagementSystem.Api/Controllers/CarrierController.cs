using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Carrier;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[Route("api/carriers")]
public class CarrierController : ApiController
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
        var carrierOrError = await _carrierService.GetById(id);

        return carrierOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var carrierOrError = await _carrierService.GetByName(name);

        return carrierOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet("{id:int}/trains")]
    public async Task<IActionResult> GetAllCarrierTrains([FromRoute] int id)
    {
        var trainsOrError = await _trainService.GetByCarrierId(id);

        return trainsOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var carriersOrError = await _carrierService.GetAll();

        return carriersOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCarrier([FromBody] CreateCarrier createCarrier)
    {
        var carrierOrError = await _carrierService.AddCarrier(createCarrier);

        return carrierOrError.Match(
            value => Created($"api/careers/{value.Id}", null),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var carrierOrError = await _carrierService.Delete(id);

        return carrierOrError.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }
}