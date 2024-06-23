using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class PassengerWithGivenEmailAlreadyExistsException(string email)
    : CustomException(message: $"Passenger with email: {email} already exists.", httpStatusCode: 400)
{
    public string Email { get; } = email;
}