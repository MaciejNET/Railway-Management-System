using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class SeatNotFoundException(Guid? id)
    : CustomException(message: $"Seat with Id: {id} does not exists.", httpStatusCode: 404)
{
    public Guid? Id { get; } = id;
}