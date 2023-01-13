using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Passenger;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

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

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var passenger = await _passengerService.GetById(id);

        if (passenger.Success is false)
        {
            return NotFound(passenger.Message);
        }

        return Ok(passenger.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var passengers = await _passengerService.GetAll();

        if (passengers.Success is false)
        {
            return NotFound(passengers.Message);
        }

        return Ok(passengers.Data);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterPassenger registerPassenger)
    {
        var passenger = await _passengerService.Register(registerPassenger);

        if (passenger.Success is false)
        {
            return BadRequest(passenger.Message);
        }
        
        if (passenger.Data is null)
        {
            return StatusCode(500);
        }

        return Created($"passengers/{passenger.Data.Id}", null);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginPassenger loginPassenger)
    {
        var passenger = await _passengerService.Login(loginPassenger);

        if (passenger.Success is false)
        {
            return BadRequest(passenger.Message);
        }

        return Ok(passenger.Data);
    }

    [Authorize(Roles = "Passenger,Admin")]
    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdatePassenger updatePassenger)
    {
        var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (user is null)
        {
            return Unauthorized("No user login");
        }

        int passengerId;
        
        if (updatePassenger.Id is not null)
        {
            passengerId = (int) updatePassenger.Id;
        }
        else
        {
            passengerId = int.Parse(user.Value);
        }

        var passenger = await _passengerService.Update(passengerId, updatePassenger);

        if (passenger.Success is false)
        {
            return BadRequest(passenger.Message);
        }

        return NoContent();
    }

    [Authorize(Roles = "Passenger,Admin")]
    [HttpPatch("updateDiscount")]
    public async Task<IActionResult> UpdateDiscount([FromBody] UpdatePassengerDiscount updatePassenger)
    {
        var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (user is null)
        {
            return Unauthorized("No user login");
        }

        int passengerId;
        
        if (updatePassenger.Id is not null)
        {
            passengerId = (int) updatePassenger.Id;
        }
        else
        {
            passengerId = int.Parse(user.Value);
        }
        
        var passenger = await _passengerService.UpdateDiscount(passengerId, updatePassenger.DiscountName);

        if (passenger.Success is false)
        {
            return BadRequest(passenger.Message);
        }

        return NoContent();
    }

    [Authorize(Roles = "Passenger,Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var passenger = await _passengerService.Delete(id);

        if (passenger.Success is false)
        {
            return BadRequest(passenger.Message);
        }

        return NoContent();
    }
}