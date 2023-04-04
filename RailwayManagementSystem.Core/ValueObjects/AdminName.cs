using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record AdminName
{
    private AdminName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<AdminName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(description: "Admin name cannot be empty.");
        }

        return new AdminName(name);
    }

    public static implicit operator string(AdminName adminName) => adminName.Value;
}