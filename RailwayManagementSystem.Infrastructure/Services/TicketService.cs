using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public class TicketService : ITicketService
{
    private readonly IMapper _mapper;
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
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