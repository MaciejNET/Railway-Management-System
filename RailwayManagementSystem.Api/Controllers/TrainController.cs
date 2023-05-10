using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.Train;
using RailwayManagementSystem.Application.Services.Abstractions;

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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var train = await _trainService.GetById(id);

        return Ok(train);
    }

    [HttpGet("names/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var train = await _trainService.GetByTrainName(name);

        return Ok(train);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trains = await _trainService.GetAll();

        return Ok(trains);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddTrain([FromBody] CreateTrain createTrain)
    {
        var train = await _trainService.AddTrain(createTrain);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _trainService.Delete(id);

        return NoContent();
    }
}