namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidLastNameException : CustomException
{
    public InvalidLastNameException() : base(message: "Last name is invalid.", httpStatusCode: 400)
    {
    }
}