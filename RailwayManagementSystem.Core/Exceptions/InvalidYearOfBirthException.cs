namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidYearOfBirthException : CustomException
{
    public InvalidYearOfBirthException() : base(message: "The provided year of birth is invalid or out of range.", httpStatusCode: 400)
    {
    }
}