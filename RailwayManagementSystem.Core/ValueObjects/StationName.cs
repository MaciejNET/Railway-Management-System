using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record StationName
{
    private StationName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<StationName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(description: "Station name cannot be empty.");
        }

        return new StationName(name);
    }

    public static implicit operator string(StationName stationName) => stationName.Value;
}