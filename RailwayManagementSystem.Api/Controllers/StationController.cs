using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.Station;
using RailwayManagementSystem.Application.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[Route("api/stations")]
public class StationController : ApiController
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
        var stationOrError = await _stationService.GetById(id);

        return stationOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet("names/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var stationOrError = await _stationService.GetByName(name);

        return stationOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stationsOrError = await _stationService.GetAll();

        return stationsOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet("cities/{city}")]
    public async Task<IActionResult> GetByCity(string city)
    {
        var stationsOrError = await _stationService.GetByCity(city);

        return stationsOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddStation([FromBody] CreateStation createStation)
    {
        var stationOrError = await _stationService.AddStation(createStation);

        return stationOrError.Match(
            value => Created($"api/stations/{value.Id}", null),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var stationOrError = await _stationService.Delete(id);
        
        return stationOrError.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }
}