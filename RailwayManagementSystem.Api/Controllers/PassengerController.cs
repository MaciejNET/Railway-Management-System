using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.Services.Abstractions;

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

        return Ok(passenger);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var passengers = await _passengerService.GetAll();

        return Ok(passengers);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterPassenger registerPassenger)
    {
        var passenger = await _passengerService.Register(registerPassenger);

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginPassenger loginPassenger)
    {
        var passenger = await _passengerService.Login(loginPassenger);

        return NoContent();
    }

    [Authorize(Roles = "Passenger,Admin")]
    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdatePassenger updatePassenger)
    {
        var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (user is null)
        {
            return Unauthorized("No user logged in");
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
        
        await _passengerService.UpdateDiscount(passengerId, updatePassenger.DiscountName);

        return NoContent();
    }

    [Authorize(Roles = "Passenger,Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _passengerService.Delete(id);

        return NoContent();
    }
}