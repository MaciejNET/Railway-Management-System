using System.Text.RegularExpressions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record PhoneNumber
{
    private const string PhoneNumberPattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$";

    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new Exception("Phone number cannot be empty");

        if (Regex.IsMatch(value, PhoneNumberPattern) is false) throw new Exception("Incorrect phone number");

        Value = value;
    }

    public string Value { get; }

    public static implicit operator PhoneNumber(string value) => new(value);

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
}