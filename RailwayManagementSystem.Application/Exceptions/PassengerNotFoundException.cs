using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class PassengerNotFoundException : CustomException
{
    public int Id { get; }

    public PassengerNotFoundException(int id) : base(message: $"Passenger with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
}