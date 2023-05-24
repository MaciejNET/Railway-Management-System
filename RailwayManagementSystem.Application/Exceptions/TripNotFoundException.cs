using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TripNotFoundException : CustomException
{
    public Guid Id { get; }

    public TripNotFoundException(Guid id) : base(message: $"Trip with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
}