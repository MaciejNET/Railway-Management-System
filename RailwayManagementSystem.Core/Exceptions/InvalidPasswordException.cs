namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidPasswordException() : CustomException(message: "Invalid password.", httpStatusCode: 400);