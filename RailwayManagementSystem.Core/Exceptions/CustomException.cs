namespace RailwayManagementSystem.Core.Exceptions;

public abstract class CustomException(string message, int httpStatusCode) : Exception(message)
{
    public int HttpStatusCode { get; } = httpStatusCode;
}