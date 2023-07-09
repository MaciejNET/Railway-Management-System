using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TicketNotAssignToThePassengerException : CustomException
{
    public Guid TicketId { get; }
    public Guid PassengerId { get; }

    public TicketNotAssignToThePassengerException(Guid ticketId, Guid passengerId) : base(message: $"Ticket with Id: {ticketId} is not assign to passenger with Id: {passengerId}", httpStatusCode: 400)
    {
        TicketId = ticketId;
        PassengerId = passengerId;
    }
}