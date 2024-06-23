namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidStationNameException()
    : CustomException(message: "Station name is invalid.", httpStatusCode: 400);