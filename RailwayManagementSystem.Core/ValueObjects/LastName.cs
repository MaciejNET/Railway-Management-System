using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record LastName
{
    public string Value { get; private set; }
    
    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidLastNameException();
        }

        Value = value;
    }

    public static implicit operator string(LastName lastName) => lastName.Value;

    public static implicit operator LastName(string value) => new(value);
}