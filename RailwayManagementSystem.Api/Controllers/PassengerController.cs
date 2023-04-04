using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Passenger;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[Route("api/passengers")]
public class PassengerController : ApiController
{
    private readonly IPassengerService _passengerService;

    public PassengerController(IPassengerService passengerService)
    {
        _passengerService = passengerService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var passengerOrError = await _passengerService.GetById(id);

        return passengerOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var passengersOrError = await _passengerService.GetAll();

        return passengersOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterPassenger registerPassenger)
    {
        var passengerOrError = await _passengerService.Register(registerPassenger);

        return passengerOrError.Match(
            value => Created($"passengers/{value.Id}", null),
            errors => Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginPassenger loginPassenger)
    {
        var passengerOrError = await _passengerService.Login(loginPassenger);

        return passengerOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
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

        var passengerOrError = await _passengerService.Update(passengerId, updatePassenger);

        return passengerOrError.Match(
            _ => NoContent(),
            errors => Problem(errors));
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
        
        var passengerOrError = await _passengerService.UpdateDiscount(passengerId, updatePassenger.DiscountName);

        return passengerOrError.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Passenger,Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var passengerOrError = await _passengerService.Delete(id);

        return passengerOrError.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }
}