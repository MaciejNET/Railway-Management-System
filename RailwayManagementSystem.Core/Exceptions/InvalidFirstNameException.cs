namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidFirstNameException : CustomException
{
    public InvalidFirstNameException() : base(message: "First name is invalid.", httpStatusCode: 400)
    {
    }
}