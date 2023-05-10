using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TicketNotFoundException : CustomException
{
    public TicketNotFoundException(int id) : base(message: $"Ticket with Id: {id} does not exists.", httpStatusCode: 404)
    {
    }
}