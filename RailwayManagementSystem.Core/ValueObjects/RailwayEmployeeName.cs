using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record RailwayEmployeeName
{
    private RailwayEmployeeName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<RailwayEmployeeName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(description: "Railway employee name cannot be empty.");
        }

        return new RailwayEmployeeName(name);
    }

    public static implicit operator string(RailwayEmployeeName railwayEmployeeName) => railwayEmployeeName.Value;
}