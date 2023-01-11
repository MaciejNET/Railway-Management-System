using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Server;
using Org.BouncyCastle.Bcpg;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Services;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById (int id)
    {
        var ticket = await _ticketService.GetById(id);

        if (ticket.Success is false)
        {
            return NotFound(ticket.Message);
        }

        return Ok(ticket.Data);
    }

    [HttpGet("{id}/pdf")]
    public async Task<IActionResult> GetTicketPdf(int id)
    {
        var ticketPdf = await _ticketService.GetTicketPdf(id);

        if (ticketPdf.Success is false)
        {
            return NotFound(ticketPdf.Message);
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
    [HttpGet("{id}/verify")]
    public async Task<IActionResult> VerifyTicket(int id)
    {
        var ticket = await _ticketService.VerifyTicket(id);

        if (ticket.Success is false)
        {
            return BadRequest(ticket.Message);
        }

        return Ok(ticket.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelTicket(int id)
    {
        var ticket = await _ticketService.Cancel(id);

        if (ticket.Success is false)
        {
            return BadRequest(ticket.Message);
        }

        return NoContent();
    }
}