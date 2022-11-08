using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/trains")]
public class TrainController : ControllerBase
{
    private readonly ITrainService _trainService;

    public TrainController(ITrainService trainService)
    {
        _trainService = trainService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var train = await _trainService.GetById(id);

        if (train.Success is false) return NotFound(train.Message);

        return Ok(train.Data);
    }

    [HttpGet("names/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var train = await _trainService.GetByTrainName(name);

        if (train.Success is false) return NotFound(train.Message);

        return Ok(train.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trains = await _trainService.GetAll();

        if (trains.Success is false) return NotFound(trains.Message);

        return Ok(trains.Data);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddTrain(TrainDto trainDto)
    {
        var train = await _trainService.AddTrain(trainDto);

        if (train.Success is false) return BadRequest(train.Message);

        return Created($"api/trains/{train.Data.Id}", null);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var train = await _trainService.Delete(id);

        if (train.Success is false) return BadRequest(train.Message);

        return NoContent();
    }
}