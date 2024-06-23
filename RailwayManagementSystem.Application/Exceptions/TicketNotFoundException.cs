using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TicketNotFoundException(Guid id)
    : CustomException(message: $"Ticket with Id: {id} does not exists.", httpStatusCode: 404)
{
    public Guid Id { get; } = id;
}