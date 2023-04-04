using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Api.Controllers;

[Route("api/tickets")]
public class TicketController : ApiController
{
    private readonly ITicketService _ticketService;

    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById ([FromRoute] int id)
    {
        var ticketOrError = await _ticketService.GetById(id);

        return ticketOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet("{id:int}/pdf")]
    public async Task<IActionResult> GetTicketPdf([FromRoute] int id)
    {
        var ticketPdfOrError = await _ticketService.GetTicketPdf(id);

        return ticketPdfOrError.Match(
            value => File(value, "appllication/pdf", "ticket.pdf"),
            errors => Problem(errors));
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
        
        var ticketsOrError = await _ticketService.GetByPassengerId(id);

        return ticketsOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }
    
    [Authorize(Roles = "Admin,RailwayEmployee")]
    [HttpGet("{id:int}/verify")]
    public async Task<IActionResult> VerifyTicket([FromRoute] int id)
    {
        var ticketOrError = await _ticketService.VerifyTicket(id);

        return ticketOrError.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> CancelTicket([FromRoute] int id)
    {
        var ticketOrError = await _ticketService.Cancel(id);

        return ticketOrError.Match(
            _ => NoContent(),
            errors => Problem(errors));
    }
}