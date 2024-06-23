namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidRailwayEmployeeNameException()
    : CustomException(message: "Railway employee name is invalid.", httpStatusCode: 400);