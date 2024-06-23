namespace RailwayManagementSystem.Core.Exceptions;

public sealed class SeatNoAvailableException()
    : CustomException(message: "This seat is no available.", httpStatusCode: 400);