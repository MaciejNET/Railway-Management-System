namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidLastNameException() : CustomException(message: "Last name is invalid.", httpStatusCode: 400);