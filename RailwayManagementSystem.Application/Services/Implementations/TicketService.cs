using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Services.Implementations;

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

    public async Task<TicketDto> GetById(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            throw new TicketNotFoundException(id);
        }

        return _mapper.Map<TicketDto>(ticket);
    }

    public async Task<byte[]> GetTicketPdf(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            throw new TicketNotFoundException(id);
        }

        return _pdfCreator.CreateTicketPdf(ticket);
    }

    public async Task<VerifyTicketResponse> VerifyTicket(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            throw new TicketNotFoundException(id);
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

    public async Task<IEnumerable<TicketDto>> GetByPassengerId(int id)
    {
        var tickets = await _ticketRepository.GetByPassengerIdAsync(id);

        if (!tickets.Any())
        {
            return new List<TicketDto>();
        }

        var ticketsDto = _mapper.Map<IEnumerable<TicketDto>>(tickets);

        return ticketsDto.ToList();
    }

    public async Task Cancel(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            throw new TicketNotFoundException(id);
        }
        
        await _ticketRepository.RemoveAsync(ticket);
    }
}