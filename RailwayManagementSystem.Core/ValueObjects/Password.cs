using System.Text.RegularExpressions;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record Password
{
    public string Value { get; }
        
    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidPasswordException();
        }
            
        Value = value;
    }

    public static implicit operator Password(string value) => new(value);

    public static implicit operator string(Password value) => value.Value;

    public override string ToString() => Value;
    
    public static bool ValidatePassword(string password)
    {
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
        
        return !string.IsNullOrWhiteSpace(password) &&
               password.Length is < 200 and > 6 &&
               password.Any(char.IsLetter) &&
               password.Any(char.IsDigit) &&
               password.Any(char.IsUpper) &&
               password.Any(char.IsLower) &&
               hasSymbols.IsMatch(password);
    }
}