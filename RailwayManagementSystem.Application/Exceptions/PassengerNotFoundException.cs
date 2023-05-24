using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class PassengerNotFoundException : CustomException
{
    public Guid Id { get; }

    public PassengerNotFoundException(Guid id) : base(message: $"Passenger with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
}