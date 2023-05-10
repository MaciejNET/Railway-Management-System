namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidEmailException : CustomException
{
    public InvalidEmailException() : base(message: "Email address is invalid.", httpStatusCode: 400)
    {
    }
}