namespace RailwayManagementSystem.Core.Exceptions;

public sealed class NoSeatsAvailableForReservationException : CustomException
{
    public NoSeatsAvailableForReservationException() : base(message: "There is no seats available to reserve.", httpStatusCode: 404)
    {
    }
}