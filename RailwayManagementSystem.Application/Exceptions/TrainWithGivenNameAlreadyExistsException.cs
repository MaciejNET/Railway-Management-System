using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TrainWithGivenNameAlreadyExistsException(string name)
    : CustomException(message: $"Train with name: {name} already exists.", httpStatusCode: 400)
{
    public string Name { get; } = name;
}