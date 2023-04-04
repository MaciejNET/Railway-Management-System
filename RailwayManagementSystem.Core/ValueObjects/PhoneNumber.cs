using System.Text.RegularExpressions;
using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record PhoneNumber
{
    private const string PhoneNumberPattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$";

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<PhoneNumber> Create(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return Error.Validation(description: "Phone number cannot be empty.");
        }

        if (!Regex.IsMatch(phoneNumber, PhoneNumberPattern))
        {
            return Error.Validation(description: "Incorrect phone number.");
        }

        return new PhoneNumber(phoneNumber);
    }

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
}