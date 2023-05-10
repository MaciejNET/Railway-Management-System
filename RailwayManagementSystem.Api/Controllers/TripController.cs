using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Commands.Ticket;
using RailwayManagementSystem.Application.Commands.Trip;
using RailwayManagementSystem.Application.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[Route("api/trips")]
public class TripController : ApiController
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
        var tripOrError = await _tripService.GetById(id);

        return tripOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tripsOrError = await _tripService.GetAll();

        return tripsOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet("{id:int}/schedule")]
    public async Task<IActionResult> GetTripSchedule([FromRoute] int id)
    {
        var scheduleOrError = await _scheduleService.GetByTripId(id);

        return scheduleOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpPost("find")]
    public async Task<IActionResult> GetConnectionTrip(ConnectionTrip connectionTrip)
    {
        var tripsOrError = await _tripService.GetConnectionTrip(connectionTrip.StartStation, connectionTrip.EndStation,
            connectionTrip.Date);

        return tripsOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddTrip(CreateTrip createTrip)
    {
        var tripOrError = await _tripService.AddTrip(createTrip);

        return tripOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
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
        
        var ticketOrError = await _bookingService.BookTicket(bookTicket, passengerId);

        return ticketOrError.Match(
            value => Created($"api/passengers/{passengerId}/tickets", value),
            errors => Problem(errors));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var tripOrError = await _tripService.Delete(id);

        return tripOrError.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }
}