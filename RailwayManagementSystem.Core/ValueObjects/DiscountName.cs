using ErrorOr;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record DiscountName
{
    public string Value { get; private set; }
    
    public DiscountName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidDiscountNameException();
        }
        Value = value;
    }

    public static implicit operator string(DiscountName discountName) => discountName.Value;

    public static implicit operator DiscountName(string value) => new(value);
}