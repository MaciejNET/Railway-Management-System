using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TicketNotFoundException : CustomException
{
    public Guid Id { get; }

    public TicketNotFoundException(Guid id) : base(message: $"Ticket with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
}