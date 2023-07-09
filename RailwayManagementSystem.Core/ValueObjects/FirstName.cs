using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record FirstName
{
    public string Value { get; private set; }
    
    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidFirstNameException();
        }

        Value = value;
    }

    public static implicit operator string(FirstName firstName) => firstName.Value;

    public static implicit operator FirstName(string value) => new(value);
}