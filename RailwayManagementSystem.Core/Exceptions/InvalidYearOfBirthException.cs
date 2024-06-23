namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidYearOfBirthException()
    : CustomException(message: "The provided year of birth is invalid or out of range.", httpStatusCode: 400);