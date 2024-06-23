namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidDateRangeException()
    : CustomException(message: "The provided date range is invalid.", httpStatusCode: 400);