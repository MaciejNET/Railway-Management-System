using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Passenger;
using RailwayManagementSystem.Infrastructure.Services;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/passengers")]
public class PassengerController : ControllerBase
{
    private readonly IPassengerService _passengerService;

    public PassengerController(IPassengerService passengerService)
    {
        _passengerService = passengerService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var passenger = await _passengerService.GetById(id);

        if (passenger.Success is false) return NotFound(passenger.Message);

        return Ok(passenger.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var passengers = await _passengerService.GetAll();

        if (passengers.Success is false) return NotFound(passengers.Message);

        return Ok(passengers.Data);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterPassenger registerPassenger)
    {
        var passenger = await _passengerService.Register(registerPassenger);

        if (passenger.Success is false) return BadRequest(passenger.Message);

        return Created($"passengers/{passenger.Data.Id}", null);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginPassenger loginPassenger)
    {
        var passenger = await _passengerService.Login(loginPassenger);

        if (passenger.Success is false) return BadRequest(passenger.Message);

        return Ok(passenger.Data);
    }

    [Authorize(Roles = "Passenger,Admin")]
    [HttpPatch("{id}/update")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePassenger updatePassenger)
    {
        var passenger = await _passengerService.Update(id, updatePassenger);

        if (passenger.Success is false) return BadRequest(passenger.Message);

        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id}/updateDiscount")]
    public async Task<IActionResult> UpdateDiscount(int id, [FromBody] string? discountName)
    {
        var passenger = await _passengerService.UpdateDiscount(id, discountName);

        if (passenger.Success is false) return BadRequest(passenger.Message);

        return NoContent();
    }

    [Authorize(Roles = "Passenger,Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var passenger = await _passengerService.Delete(id);

        if (passenger.Success is false) return BadRequest(passenger.Message);

        return NoContent();
    }
}