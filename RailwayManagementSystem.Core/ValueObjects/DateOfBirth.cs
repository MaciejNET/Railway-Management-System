using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record DateOfBirth
{
    public DateOnly Value { get; }

    public DateOfBirth(DateOnly value)
    {
        var currentDate = DateTime.Now;

        if (value.Year < 1900 || value.Year > currentDate.Year)
        {
            throw new InvalidYearOfBirthException();
        }

        var age = currentDate.Year - value.Year;

        if (currentDate.Month < value.Month || (currentDate.Month == value.Month && currentDate.Day < value.Day))
        {
            age--;
        }

        if (age <= 13)
        {
            throw new InvalidAgeException();
        }

        Value = value;
    }

    public static implicit operator DateOnly(DateOfBirth dateOfBirth) => dateOfBirth.Value;

    public static implicit operator DateOfBirth(DateOnly value) => new(value);
}