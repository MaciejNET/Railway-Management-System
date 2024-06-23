using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TripNotFoundException(Guid id)
    : CustomException(message: $"Trip with Id: {id} does not exists.", httpStatusCode: 404)
{
    public Guid Id { get; } = id;
}