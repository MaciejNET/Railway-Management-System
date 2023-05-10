using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class InvalidTripDateException : CustomException
{
    public int Id { get; }

    public InvalidTripDateException(int id) : base(message: $"Trip with Id: {id} does not run on given date.", httpStatusCode: 400)
    {
        Id = id;
    }
}