using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class StationWithGivenNameAlreadyExistsException(string name)
    : CustomException(message: $"Station with name: {name} already exists.", httpStatusCode: 400)
{
    public string Name { get; } = name;
}