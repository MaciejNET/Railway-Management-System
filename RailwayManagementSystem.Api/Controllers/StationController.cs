using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Station;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/stations")]
public class StationController : ControllerBase
{
    private readonly IScheduleService _scheduleService;
    private readonly IStationService _stationService;

    public StationController(IStationService stationService, IScheduleService scheduleService)
    {
        _stationService = stationService;
        _scheduleService = scheduleService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var station = await _stationService.GetById(id);

        if (station.Success is false)
        {
            return NotFound(station.Message);
        }

        return Ok(station.Data);
    }

    [HttpGet("names/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var station = await _stationService.GetByName(name);

        if (station.Success is false)
        {
            return NotFound(station.Message);
        }

        return Ok(station.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stations = await _stationService.GetAll();

        if (stations.Success is false)
        {
            return NotFound(stations.Message);
        }

        return Ok(stations.Data);
    }

    [HttpGet("cities/{city}")]
    public async Task<IActionResult> GetByCity(string city)
    {
        var stations = await _stationService.GetByCity(city);

        if (stations.Success is false)
        {
            return NotFound(stations.Message);
        }

        return Ok(stations.Data);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddStation([FromBody] CreateStation createStation)
    {
        var station = await _stationService.AddStation(createStation);

        if (station.Success is false)
        {
            return BadRequest(station.Message);
        }
        
        if (station.Data is null)
        {
            return StatusCode(500);
        }

        return Created($"api/stations/{station.Data.Id}", null);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var station = await _stationService.Delete(id);

        if (station.Success is false)
        {
            return BadRequest(station.Message);
        }

        return NoContent();
    }
}