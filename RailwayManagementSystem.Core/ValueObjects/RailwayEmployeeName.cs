using ErrorOr;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record RailwayEmployeeName
{
    public string Value { get; private set; }
    
    public RailwayEmployeeName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidRailwayEmployeeNameException();
        }

        Value = value;
    }

    public static implicit operator string(RailwayEmployeeName railwayEmployeeName) => railwayEmployeeName.Value;

    public static implicit operator RailwayEmployeeName(string value) => new(value);
}