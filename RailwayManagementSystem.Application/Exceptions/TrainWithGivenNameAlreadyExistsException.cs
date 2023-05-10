using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TrainWithGivenNameAlreadyExistsException : CustomException
{
    public string Name { get; }

    public TrainWithGivenNameAlreadyExistsException(string name) : base(message: $"Train with name: {name} already exists.", httpStatusCode: 400)
    {
        Name = name;
    }
}