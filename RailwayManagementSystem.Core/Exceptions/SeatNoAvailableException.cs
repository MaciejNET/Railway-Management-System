namespace RailwayManagementSystem.Core.Exceptions;

public sealed class SeatNoAvailableException : CustomException
{
    public SeatNoAvailableException() : base(message: "This seat is no available.", httpStatusCode: 400)
    {
    }
}