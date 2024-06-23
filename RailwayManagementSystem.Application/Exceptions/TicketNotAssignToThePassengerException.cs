using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TicketNotAssignToThePassengerException(Guid ticketId, Guid passengerId) : CustomException(
    message: $"Ticket with Id: {ticketId} is not assign to passenger with Id: {passengerId}", httpStatusCode: 400)
{
    public Guid TicketId { get; } = ticketId;
    public Guid PassengerId { get; } = passengerId;
}