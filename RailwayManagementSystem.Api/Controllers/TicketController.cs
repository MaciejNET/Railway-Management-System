using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Application.Services.Abstractions;

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

        return Ok(ticket);
    }

    [HttpGet("{id:int}/pdf")]
    public async Task<IActionResult> GetTicketPdf([FromRoute] int id)
    {
        var ticketPdf = await _ticketService.GetTicketPdf(id);

        return Ok(ticketPdf);
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

        return Ok(tickets);
    }
    
    [Authorize(Roles = "Admin,RailwayEmployee")]
    [HttpGet("{id:int}/verify")]
    public async Task<IActionResult> VerifyTicket([FromRoute] int id)
    {
        var ticket = await _ticketService.VerifyTicket(id);

        return Ok(ticket);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> CancelTicket([FromRoute] int id)
    {
        await _ticketService.Cancel(id);

        return NoContent();
    }
}