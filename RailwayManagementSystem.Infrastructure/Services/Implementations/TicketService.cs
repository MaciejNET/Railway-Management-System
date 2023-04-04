using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

public class TicketService : ITicketService
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;
    private readonly IPdfCreator _pdfCreator;

    public TicketService(ITicketRepository ticketRepository, IMapper mapper, IPdfCreator pdfCreator)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
        _pdfCreator = pdfCreator;
    }

    public async Task<ErrorOr<TicketDto>> GetById(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            return Error.NotFound($"Ticket with id: '{id}' does not exists.");
        }

        return _mapper.Map<TicketDto>(ticket);
    }

    public async Task<ErrorOr<byte[]>> GetTicketPdf(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            return Error.NotFound($"Ticket with id: '{id}' does not exists.");
        }

        return _pdfCreator.CreateTicketPdf(ticket);
    }

    public async Task<ErrorOr<VerifyTicketResponse>> VerifyTicket(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            return Error.NotFound($"Ticket with id: '{id}' does not exists.");
        }

        var today = DateOnly.FromDateTime(DateTime.Now);
        var response = new VerifyTicketResponse()
        {
            StartStation = ticket.Stations.First().Name.Value,
            EndStation = ticket.Stations.Last().Name.Value
        };
        
        if (today > ticket.TripDate)
        {
            response.IsValid = true;
            return response;
        }

        response.IsValid = false;
        return response;
    }

    public async Task<ErrorOr<IEnumerable<TicketDto>>> GetByPassengerId(int id)
    {
        var tickets = await _ticketRepository.GetByPassengerIdAsync(id);

        if (!tickets.Any())
        {
            return Error.NotFound($"Cannot find any ticket for passenger with id: '{id}'");
        }

        var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(tickets);

        return ticketsDto.ToList();
    }

    public async Task<ErrorOr<Success>> Cancel(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            return Error.NotFound($"Ticket with id: '{id}' does not exists.");
        }
        
        await _ticketRepository.RemoveAsync(ticket);

        return Result.Success;
    }
}