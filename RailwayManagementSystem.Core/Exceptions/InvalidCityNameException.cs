namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidCityNameException() : CustomException(message: "City name is invalid.", httpStatusCode: 400);