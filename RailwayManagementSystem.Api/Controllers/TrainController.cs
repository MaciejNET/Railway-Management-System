using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.Train;
using RailwayManagementSystem.Application.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[Route("api/trains")]
public class TrainController : ApiController
{
    private readonly ITrainService _trainService;

    public TrainController(ITrainService trainService)
    {
        _trainService = trainService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var trainOrError = await _trainService.GetById(id);

        return trainOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet("names/{name}")]
    public async Task<IActionResult> GetByName([FromRoute] string name)
    {
        var trainOrError = await _trainService.GetByTrainName(name);

        return trainOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trainsOrError = await _trainService.GetAll();

        return trainsOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddTrain([FromBody] CreateTrain createTrain)
    {
        var trainOrError = await _trainService.AddTrain(createTrain);
        
        return trainOrError.Match(
            value => Created($"api/trains/{value.Id}", null),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var trainOrError = await _trainService.Delete(id);

        return trainOrError.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }
}