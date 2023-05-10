using ErrorOr;
using RailwayManagementSystem.Application.Commands.Ticket;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IBookingService
{
    Task<ErrorOr<TicketDto>> BookTicket(BookTicket bookTicket, int passengerId);
}