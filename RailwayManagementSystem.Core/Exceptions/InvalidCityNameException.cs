namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidCityNameException : CustomException
{
    public InvalidCityNameException() : base(message: "City name is invalid.", httpStatusCode: 400)
    {
    }
}