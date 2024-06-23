namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidDiscountNameException()
    : CustomException(message: "Discount name is invalid.", httpStatusCode: 400);