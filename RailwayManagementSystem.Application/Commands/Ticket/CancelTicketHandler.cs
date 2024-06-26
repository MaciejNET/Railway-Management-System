using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Ticket;

internal sealed class CancelTicketHandler(ITicketRepository ticketRepository) : ICommandHandler<CancelTicket>
{
    public async Task HandleAsync(CancelTicket command)
    {
        var ticket = await ticketRepository.GetByIdAsync(command.Id);

        if (ticket is null)
        {
            throw new TicketNotFoundException(command.Id);
        }
        
        var isTicketAssignedToPassenger = ticket.PassengerId.Equals(command.PassengerId);
        
        if (isTicketAssignedToPassenger is false)
        {
            throw new TicketNotAssignToThePassengerException(command.Id, command.PassengerId);
        }

        await ticketRepository.DeleteAsync(ticket);
    }
}