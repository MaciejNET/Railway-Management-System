namespace RailwayManagementSystem.Core.ValueObjects;

public record City
{
    public City(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new Exception("City cannot be empty");
        Value = value;
    }

    public string Value { get; private set; }

    public static implicit operator City(string value)
    {
        return new(value);
    }

    public static implicit operator string(City city)
    {
        return city.Value;
    }
}