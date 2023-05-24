using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class CarrierAlreadyExistsException : CustomException
{
    public string CarrierName { get; }

    public CarrierAlreadyExistsException(string carrierName) : base(message: $"Carrier with name: {carrierName} already exists.", httpStatusCode: 400)
    {
        CarrierName = carrierName;
    }
}