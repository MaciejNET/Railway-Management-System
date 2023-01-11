using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Train;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddTrain(CreateTrain createTrain)
    {
        var train = await _trainService.AddTrain(createTrain);

        if (train.Success is false) return BadRequest(train.Message);

        return Created($"api/trains/{train.Data.Id}", null);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var train = await _trainService.Delete(id);

        if (train.Success is false) return BadRequest(train.Message);

        return NoContent();
    }
}