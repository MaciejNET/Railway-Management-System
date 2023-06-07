using System.Text.RegularExpressions;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record Password
{
    public string Value { get; }
        
    public Password(string value)
    {
        if (!ValidatePassword(value))
        {
            throw new InvalidPasswordException();
        }
            
        Value = value;
    }

    public static implicit operator Password(string value) => new(value);

    public static implicit operator string(Password value) => value.Value;

    public override string ToString() => Value;

    private bool ValidatePassword(string password)
    {
        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        return !string.IsNullOrWhiteSpace(password) &&
               password.Length is < 200 and > 6 &&
               hasNumber.IsMatch(password) &&
               hasUpperChar.IsMatch(password) &&
               hasLowerChar.IsMatch(password) &&
               hasSymbols.IsMatch(password);
    }
}