namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidDiscountNameException : CustomException
{
    public InvalidDiscountNameException() : base(message: "Discount name is invalid.", httpStatusCode: 400)
    {
    }
}