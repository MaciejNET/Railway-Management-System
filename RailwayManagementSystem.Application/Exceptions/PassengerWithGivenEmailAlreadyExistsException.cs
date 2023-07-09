using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class PassengerWithGivenEmailAlreadyExistsException : CustomException
{
    public string Email { get; }

    public PassengerWithGivenEmailAlreadyExistsException(string email) : base(message: $"Passenger with email: {email} already exists.", httpStatusCode: 400)
    {
        Email = email;
    }
}