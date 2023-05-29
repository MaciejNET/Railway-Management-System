using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ErrorOr;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record Email
{
    private static readonly Regex Regex = new(
        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
        RegexOptions.Compiled);
    
    public string Value { get; private set; }
    
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidEmailException();
        }
        
        if (!Regex.IsMatch(value))
        {
            throw new InvalidEmailException();
        }
        
        Value = value;
    }

    public static implicit operator string(Email email) => email.Value;

    public static implicit operator Email(string value) => new(value);
}