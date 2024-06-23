namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidTrainNameException()
    : CustomException(message: "Train name is invalid.", httpStatusCode: 400);