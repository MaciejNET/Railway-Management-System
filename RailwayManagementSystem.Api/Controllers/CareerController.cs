using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/careers")]
public class CareerController : ControllerBase
{
    private readonly ICareerService _careerService;
    private readonly ITrainService _trainService;

    public CareerController(ICareerService careerService, ITrainService trainService)
    {
        _careerService = careerService;
        _trainService = trainService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var career = await _careerService.GetById(id);
        
        if (career.Success is false) return NotFound(career.Message);

        return Ok(career.Data);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var career = await _careerService.GetByName(name);

        if (career.Success is false) return NotFound(career.Message);

        return Ok(career.Data);
    }

    [HttpGet("{id}/trains")]
    public async Task<IActionResult> GetAllCareerTrains(int id)
    {
        var trains = await _trainService.GetByCareerId(id);

        if (trains.Success is false) return NotFound(trains.Message);

        return Ok(trains.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var careers = await _careerService.GetAll();

        if (careers.Success is false) return NotFound(careers.Message);

        return Ok(careers.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddCareer([FromBody] CareerDto careerDto)
    {
        var career = await _careerService.AddCareer(careerDto);

        if (career.Success is false) return BadRequest(career.Message);

        return Created($"api/careers/{career.Data.Id}", null);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var career = await _careerService.Delete(id);

        if (career.Success is false) return BadRequest(career.Message);

        return NoContent();
    }
}