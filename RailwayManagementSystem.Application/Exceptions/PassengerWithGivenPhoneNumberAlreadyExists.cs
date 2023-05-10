using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class PassengerWithGivenPhoneNumberAlreadyExists : CustomException
{
    public string PhoneNumber { get; }

    public PassengerWithGivenPhoneNumberAlreadyExists(string phoneNumber) : base(message: $"Passenger with phone number: {phoneNumber} already exists.", httpStatusCode: 400)
    {
        PhoneNumber = phoneNumber;
    }
}