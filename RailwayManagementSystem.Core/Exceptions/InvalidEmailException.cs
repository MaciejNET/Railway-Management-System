namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidEmailException() : CustomException(message: "Email address is invalid.", httpStatusCode: 400);