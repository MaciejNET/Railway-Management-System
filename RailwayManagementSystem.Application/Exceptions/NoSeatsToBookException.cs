using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class NoSeatsToBookException()
    : CustomException(message: "There is no free seat to book.", httpStatusCode: 400);