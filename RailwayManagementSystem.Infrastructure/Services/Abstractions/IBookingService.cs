using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Ticket;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IBookingService
{
    Task<ErrorOr<TicketDto>> BookTicket(BookTicket bookTicket, int passengerId);
}