using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class InvalidDepartureTimeException : CustomException
{
    public string Name { get; }

    public InvalidDepartureTimeException(string name) : base(message: $"In station: {name} departure time is before arrival time.", httpStatusCode: 400)
    {
        Name = name;
    }
}