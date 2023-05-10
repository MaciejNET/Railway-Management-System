using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.Station;
using RailwayManagementSystem.Application.Services.Abstractions;

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

        return Ok(station);
    }

    [HttpGet("names/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var station = await _stationService.GetByName(name);

        return Ok(station);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stations = await _stationService.GetAll();

        return Ok(stations);
    }

    [HttpGet("cities/{city}")]
    public async Task<IActionResult> GetByCity(string city)
    {
        var stations = await _stationService.GetByCity(city);

        return Ok(stations);
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddStation([FromBody] CreateStation createStation)
    {
        var station = await _stationService.AddStation(createStation);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _stationService.Delete(id);

        return NoContent();
    }
}