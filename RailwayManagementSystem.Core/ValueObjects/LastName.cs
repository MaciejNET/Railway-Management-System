using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record LastName
{
    private LastName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<LastName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(description: "Last name cannot be empty.");
        }

        return new LastName(name);
    }

    public static implicit operator string(LastName lastName) => lastName.Value;
}