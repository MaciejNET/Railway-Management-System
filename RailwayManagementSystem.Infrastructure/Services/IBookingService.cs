using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Ticket;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface IBookingService
{
    Task<ServiceResponse<TicketDto>> BookTicket(BookTicket bookTicket);
}