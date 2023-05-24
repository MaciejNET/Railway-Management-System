namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidPasswordException : CustomException
{
    public InvalidPasswordException() : base(message: "Invalid password.", httpStatusCode: 400)
    {
    }
}