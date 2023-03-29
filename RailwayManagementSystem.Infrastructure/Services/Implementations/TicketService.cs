using AutoMapper;
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

    public async Task<ServiceResponse<TicketDto>> GetById(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

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
        var ticket = await _ticketRepository.GetByIdAsync(id);

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

    public async Task<ServiceResponse<VerifyTicketResponse>> VerifyTicket(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            var serviceResponse = new ServiceResponse<VerifyTicketResponse>
            {
                Success = false,
                Message = $"Ticket with id: '{id}' does not exists."
            };

            return serviceResponse;
        }

        var today = DateOnly.FromDateTime(DateTime.Now);
        var response = new ServiceResponse<VerifyTicketResponse>
        {
            Data = new VerifyTicketResponse()
            {
                StartStation = ticket.Stations.First().Name.Value,
                EndStation = ticket.Stations.Last().Name.Value
            }
        };
        if (today > ticket.TripDate)
        {
            response.Data.IsValid = true;
            return response;
        }

        response.Data.IsValid = false;
        return response;
    }

    public async Task<ServiceResponse<IEnumerable<TicketDto>>> GetByPassengerId(int id)
    {
        var tickets = await _ticketRepository.GetByPassengerIdAsync(id);

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

    public async Task<ServiceResponse<TicketDto>> Cancel(int id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id);

        if (ticket is null)
        {
            var serviceResponse = new ServiceResponse<TicketDto>
            {
                Success = false,
                Message = $"Ticket with id: '{id}' does not exists."
            };

            return serviceResponse;
        }
        
        await _ticketRepository.RemoveAsync(ticket);
        await _ticketRepository.SaveChangesAsync();

        var response = new ServiceResponse<TicketDto>
        {
            Data = _mapper.Map<TicketDto>(ticket),
            Success = true
        };

        return response;
    }
}