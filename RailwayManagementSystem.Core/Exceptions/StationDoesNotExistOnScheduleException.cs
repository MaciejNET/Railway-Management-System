namespace RailwayManagementSystem.Core.Exceptions;

public sealed class StationDoesNotExistOnScheduleException()
    : CustomException(message: "Station does not exists on trip's schedule.", httpStatusCode: 400);