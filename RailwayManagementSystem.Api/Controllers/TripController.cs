using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.Ticket;
using RailwayManagementSystem.Application.Commands.Trip;
using RailwayManagementSystem.Application.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/trips")]
public class TripController : ControllerBase
{
    private readonly IBookingService _bookingService;
    private readonly IScheduleService _scheduleService;
    private readonly ITripService _tripService;

    public TripController(ITripService tripService, IBookingService bookingService, IScheduleService scheduleService)
    {
        _tripService = tripService;
        _bookingService = bookingService;
        _scheduleService = scheduleService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var trip = await _tripService.GetById(id);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trips = await _tripService.GetAll();

        return Ok();
    }

    [HttpGet("{id:int}/schedule")]
    public async Task<IActionResult> GetTripSchedule([FromRoute] int id)
    {
        var schedule = await _scheduleService.GetByTripId(id);

        return Ok();
    }

    [HttpPost("find")]
    public async Task<IActionResult> GetConnectionTrip(ConnectionTrip connectionTrip)
    {
        var trips = await _tripService.GetConnectionTrip(connectionTrip.StartStation, connectionTrip.EndStation,
            connectionTrip.Date);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddTrip(CreateTrip createTrip)
    {
        var trip = await _tripService.AddTrip(createTrip);

        return NoContent();
    }

    [Authorize(Roles = "Passenger")]
    [HttpPost("book")]
    public async Task<IActionResult> BookTicket([FromBody] BookTicket bookTicket)
    {
        var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (user is null)
        {
            return Unauthorized("No user login");
        }

        var passengerId = int.Parse(user.Value);
        
        var ticket = await _bookingService.BookTicket(bookTicket, passengerId);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _tripService.Delete(id);

        return NoContent();
    }
}