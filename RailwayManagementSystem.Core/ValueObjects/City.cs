using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record City
{
    public string Value { get; private set; }
    
    public City(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidCityNameException();
        }
        Value = value;
    }

    public static implicit operator string(City city) => city.Value;

    public static implicit operator City(string value) => new(value);
}