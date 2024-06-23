namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidNewStationDepartureTimeException() : CustomException(
    message: "Departure time should be greater than the previous station's arrival time.", httpStatusCode: 400);