using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base(message: "Invalid credentials", httpStatusCode: 400)
    {
    }
}