using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class InvalidCredentialsException() : CustomException(message: "Invalid credentials", httpStatusCode: 400);