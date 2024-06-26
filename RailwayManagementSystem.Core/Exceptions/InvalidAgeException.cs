namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidAgeException()
    : CustomException(message: "You must be at least 13 years old to create an account.", httpStatusCode: 400);