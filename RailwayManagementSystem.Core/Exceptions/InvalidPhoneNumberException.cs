namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidPhoneNumberException : CustomException
{
    public InvalidPhoneNumberException() : base(message: "Phone number is invalid.", httpStatusCode: 400)
    {
    }
}