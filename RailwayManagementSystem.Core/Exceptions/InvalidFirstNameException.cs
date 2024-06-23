namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidFirstNameException()
    : CustomException(message: "First name is invalid.", httpStatusCode: 400);