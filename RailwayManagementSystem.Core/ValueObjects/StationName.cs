using ErrorOr;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record StationName
{
    public string Value { get; private set; }
    
    public StationName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidStationNameException();
        }

        Value = value;
    }

    public static implicit operator string(StationName stationName) => stationName.Value;

    public static implicit operator StationName(string value) => new(value);
}