using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class InvalidTripDateException(Guid id)
    : CustomException(message: $"Trip with Id: {id} does not run on given date.", httpStatusCode: 400)
{
    public Guid Id { get; } = id;
}