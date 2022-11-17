using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

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

    public async Task<ServiceResponse<TicketDto>> GetById(int id)
    {
        var ticket = await _ticketRepository.GetById(id);

        if (ticket is null)
        {
            var serviceResponse = new ServiceResponse<TicketDto>
            {
                Success = false,
                Message = $"Ticket with id: '{id}' does not exists."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<TicketDto>
        {
            Data = _mapper.Map<TicketDto>(ticket)
        };

        return response;
    }

    public async Task<ServiceResponse<byte[]>> GetTicketPdf(int id)
    {
        var ticket = await _ticketRepository.GetById(id);

        if (ticket is null)
        {
            var serviceResponse = new ServiceResponse<byte[]>
            {
                Success = false,
                Message = $"Ticket with id: '{id}' does not exists."
            };

            return serviceResponse;
        }

        var pdf = _pdfCreator.CreateTicketPdf(ticket);
        var response = new ServiceResponse<byte[]>
        {
            Data = pdf
        };

        return response;
    }

    public async Task<ServiceResponse<string>> VerifyTicket(int id)
    {
        var ticket = await _ticketRepository.GetById(id);

        if (ticket is null)
        {
            var serviceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = $"Ticket with id: '{id}' does not exists."
            };

            return serviceResponse;
        }

        var today = DateOnly.FromDateTime(DateTime.Now);
        var response = new ServiceResponse<string>();
        if (today > ticket.TripDate)
        {
            response.Data = "Invalid ticket";
            return response;
        }

        response.Data = "Valid ticket";
        return response;
    }

    public async Task<ServiceResponse<IEnumerable<TicketDto>>> GetByPassengerId(int id)
    {
        var tickets = await _ticketRepository.GetByPassengerId(id);

        if (tickets.Any() is false)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<TicketDto>>
            {
                Success = false,
                Message = $"Cannot find any ticket for passenger with id: '{id}'"
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<IEnumerable<TicketDto>>
        {
            Data = _mapper.Map<IEnumerable<TicketDto>>(tickets)
        };

        return response;
    }

    public async Task Delete(int id)
    {
        var ticket = await _ticketRepository.GetById(id);
        await _ticketRepository.Remove(ticket);
    }
}