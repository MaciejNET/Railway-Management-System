namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidDepartureTimeException(string name)
    : CustomException(message: $"In station: {name} departure time is before arrival time.", httpStatusCode: 400)
{
    public string Name { get; } = name;
}