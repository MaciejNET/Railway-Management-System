using System.ComponentModel.DataAnnotations;
using ErrorOr;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record Email
{
    public string Value { get; private set; }
    
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEmailException();
        }
        
        if (!new EmailAddressAttribute().IsValid(value))
        {
            throw new InvalidEmailException();
        }
        
        Value = value;
    }

    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string value) => new(value);
}