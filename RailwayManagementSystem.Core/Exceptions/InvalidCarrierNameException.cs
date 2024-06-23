namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidCarrierNameException()
    : CustomException(message: "Carrier name is invalid.", httpStatusCode: 400);