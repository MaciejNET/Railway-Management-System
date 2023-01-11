using System.ComponentModel.DataAnnotations;

namespace RailwayManagementSystem.Core.ValueObjects;

public record Email
{
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new Exception("Email cannot be empty");

        var isValid = new EmailAddressAttribute().IsValid(value);

        if (isValid is false) throw new Exception("Email structure is invalid");

        Value = value;
    }

    public string Value { get; }

    public static implicit operator Email(string value) => new(value);

    public static implicit operator string(Email email) => email.Value;
}