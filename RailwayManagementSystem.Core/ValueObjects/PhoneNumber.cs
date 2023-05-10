using System.Text.RegularExpressions;
using ErrorOr;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record PhoneNumber
{
    private const string PhoneNumberPattern = @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$";

    public string Value { get; private set; }
    
    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPhoneNumberException();
        }
        
        if (!Regex.IsMatch(value, PhoneNumberPattern))
        {
            throw new InvalidPhoneNumberException();
        }
        
        Value = value;
    }

    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;

    public static implicit operator PhoneNumber(string value) => new(value);
}