using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class InvalidBookTicketDateException : CustomException
{
    public InvalidBookTicketDateException() : base(message: "Cannot book ticket for past date.", httpStatusCode: 400)
    {
    }
}