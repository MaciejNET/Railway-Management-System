using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class CarrierAlreadyExistsException(string carrierName)
    : CustomException(message: $"Carrier with name: {carrierName} already exists.", httpStatusCode: 400)
{
    public string CarrierName { get; } = carrierName;
}