namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidAdminNameException()
    : CustomException(message: "Admin name is invalid.", httpStatusCode: 400);