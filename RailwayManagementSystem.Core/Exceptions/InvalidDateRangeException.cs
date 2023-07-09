namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidDateRangeException : CustomException
{
    public InvalidDateRangeException() : base(message: "The provided date range is invalid.", httpStatusCode: 400)
    {
    }
}