namespace RailwayManagementSystem.Core.Exceptions;

public sealed class NoSeatsAvailableForReservationException()
    : CustomException(message: "There is no seats available to reserve.", httpStatusCode: 404);