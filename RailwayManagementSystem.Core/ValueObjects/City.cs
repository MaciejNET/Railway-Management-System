using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record City
{
    private City(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<City> Create(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
        {
            return Error.Validation(description: "City cannot be empty.");
        }

        return new City(city);
    }

    public static implicit operator string(City city) => city.Value;
}