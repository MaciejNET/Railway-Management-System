using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class StationWithGivenNameAlreadyExistsException : CustomException
{
    public string Name { get; }

    public StationWithGivenNameAlreadyExistsException(string name) : base(message: $"Station with name: {name} already exists.", httpStatusCode: 400)
    {
        Name = name;
    }
}