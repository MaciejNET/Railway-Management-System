using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record FirstName
{
    private FirstName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<FirstName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(description: "First name cannot be empty.");
        }

        return new FirstName(name);
    }

    public static implicit operator string(FirstName firstName) => firstName.Value;
}