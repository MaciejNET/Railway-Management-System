using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class PassengerNotFoundException(Guid id)
    : CustomException(message: $"Passenger with Id: {id} does not exists.", httpStatusCode: 404)
{
    public Guid Id { get; } = id;
}