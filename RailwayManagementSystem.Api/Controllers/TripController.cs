using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Commands.Ticket;
using RailwayManagementSystem.Infrastructure.Commands.Trip;
using RailwayManagementSystem.Infrastructure.Services;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var trip = await _tripService.GetById(id);

        if (trip.Success is false) return NotFound(trip.Message);

        return Ok(trip.Data);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var trips = await _tripService.GetAll();

        if (trips.Success is false) return NotFound(trips.Message);

        return Ok(trips.Data);
    }

    [HttpGet("{id}/schedule")]
    public async Task<IActionResult> GetTripSchedule(int id)
    {
        var schedule = await _scheduleService.GetByTripId(id);

        if (schedule.Success is false) return NotFound(schedule.Message);

        return Ok(schedule.Data);
    }

    [HttpPost("find")]
    public async Task<IActionResult> GetConnectionTrip(ConnectionTrip connectionTrip)
    {
        var trips = await _tripService.GetConnectionTrip(connectionTrip.StartStation, connectionTrip.EndStation,
            connectionTrip.Date);

        if (trips.Success is false) return BadRequest(trips.Message);

        return Ok(trips.Data);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddTrip(CreateTrip createTrip)
    {
        var trip = await _tripService.AddTrip(createTrip);

        if (trip.Success is false) return BadRequest(trip.Message);

        return Created($"api/trips/{trip.Data.Id}", null);
    }

    [Authorize]
    [HttpPost("book")]
    public async Task<IActionResult> BookTicket(BookTicket bookTicket)
    {
        var ticket = await _bookingService.BookTicket(bookTicket);

        if (ticket.Success is false) return BadRequest(ticket.Message);

        return Created($"api/passengers/{bookTicket.PassengerId}/tickets", ticket.Data);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var trip = await _tripService.Delete(id);

        if (trip.Success is false) return BadRequest(trip.Message);

        return NoContent();
    }
}