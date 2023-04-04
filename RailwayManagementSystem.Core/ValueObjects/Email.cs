using System.ComponentModel.DataAnnotations;
using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record Email
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<Email> Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Error.Validation(description: "Email cannot be empty.");
        }

        if (!new EmailAddressAttribute().IsValid(email))
        {
            return Error.Validation(description: "Email structure is invalid");
        }

        return new Email(email);
    }

    public static implicit operator string(Email email) => email.Value;
}