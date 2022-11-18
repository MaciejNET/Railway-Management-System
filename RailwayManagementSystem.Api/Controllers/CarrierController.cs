using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var carrier = await _carrierService.GetById(id);
        
        if (carrier.Success is false) return NotFound(carrier.Message);

        return Ok(carrier.Data);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var carrier = await _carrierService.GetByName(name);

        if (carrier.Success is false) return NotFound(carrier.Message);

        return Ok(carrier.Data);
    }

    [HttpGet("{id}/trains")]
    public async Task<IActionResult> GetAllCarrierTrains(int id)
    {
        var trains = await _trainService.GetByCarrierId(id);

        if (trains.Success is false) return NotFound(trains.Message);

        return Ok(trains.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var carriers = await _carrierService.GetAll();

        if (carriers.Success is false) return NotFound(carriers.Message);

        return Ok(carriers.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCarrier([FromBody] CarrierDto careerDto)
    {
        var carrier = await _carrierService.AddCarrier(careerDto);

        if (carrier.Success is false) return BadRequest(carrier.Message);

        return Created($"api/careers/{carrier.Data.Id}", null);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var carrier = await _carrierService.Delete(id);

        if (carrier.Success is false) return BadRequest(carrier.Message);

        return NoContent();
    }
}