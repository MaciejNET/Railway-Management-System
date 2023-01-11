using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById ([FromRoute] int id)
    {
        var ticket = await _ticketService.GetById(id);

        if (ticket.Success is false)
        {
            return NotFound(ticket.Message);
        }

        return Ok(ticket.Data);
    }

    [HttpGet("{id:int}/pdf")]
    public async Task<IActionResult> GetTicketPdf([FromRoute] int id)
    {
        var ticketPdf = await _ticketService.GetTicketPdf(id);

        if (ticketPdf.Success is false)
        {
            return NotFound(ticketPdf.Message);
        }

        if (ticketPdf.Data is null)
        {
            return StatusCode(500);
        }
        
        return File(ticketPdf.Data, "application/pdf", "ticket.pdf");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (user is null)
        {
            return Unauthorized("No user login");
        }
        
        var id = int.Parse(user.Value);
        
        var tickets = await _ticketService.GetByPassengerId(id);
        
        if (tickets.Success is false)
        {
            return NotFound(tickets.Message);
        }

        return Ok(tickets.Data);
    }
    
    [Authorize(Roles = "Admin,RailwayEmployee")]
    [HttpGet("{id:int}/verify")]
    public async Task<IActionResult> VerifyTicket([FromRoute] int id)
    {
        var ticket = await _ticketService.VerifyTicket(id);

        if (ticket.Success is false)
        {
            return BadRequest(ticket.Message);
        }

        return Ok(ticket.Data);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> CancelTicket([FromRoute] int id)
    {
        var ticket = await _ticketService.Cancel(id);

        if (ticket.Success is false)
        {
            return BadRequest(ticket.Message);
        }

        return NoContent();
    }
}