namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidEntityIdException : CustomException
{
    public object Id { get; }

    public InvalidEntityIdException(object id) : base(message: $"Cannot set: {id} as entity identifier.", httpStatusCode: 400)
    {
        Id = id;
    }
}