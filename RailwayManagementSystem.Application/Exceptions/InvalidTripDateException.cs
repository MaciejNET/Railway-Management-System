using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class InvalidTripDateException : CustomException
{
    public Guid Id { get; }

    public InvalidTripDateException(Guid id) : base(message: $"Trip with Id: {id} does not run on given date.", httpStatusCode: 400)
    {
        Id = id;
    }
}