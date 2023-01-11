namespace RailwayManagementSystem.Core.ValueObjects;

public record Name
{
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new Exception("Name cannot be empty");
        Value = value;
    }

    public string Value { get; }

    public static implicit operator Name(string value) => new(value);

    public static implicit operator string(Name name) => name.Value;
}