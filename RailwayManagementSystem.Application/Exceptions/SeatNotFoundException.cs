using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class SeatNotFoundException : CustomException
{
    public Guid? Id { get; }

    public SeatNotFoundException(Guid? id) : base(message: $"Seat with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
}