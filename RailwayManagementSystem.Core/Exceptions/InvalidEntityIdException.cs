namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidEntityIdException(object id)
    : CustomException(message: $"Cannot set: {id} as entity identifier.", httpStatusCode: 400)
{
    public object Id { get; } = id;
}