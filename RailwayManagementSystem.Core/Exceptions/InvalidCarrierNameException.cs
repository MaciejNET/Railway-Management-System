namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidCarrierNameException : CustomException
{
    public InvalidCarrierNameException() : base(message: "Carrier name is invalid.", httpStatusCode: 400)
    {
    }
}