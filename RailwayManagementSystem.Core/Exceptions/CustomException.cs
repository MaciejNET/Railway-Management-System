namespace RailwayManagementSystem.Core.Exceptions;

public abstract class CustomException : Exception
{
    public int HttpStatusCode { get; }
    
    protected CustomException(string message, int httpStatusCode) : base(message)
    {
        HttpStatusCode = httpStatusCode;
    }
}