using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record DiscountName
{
    private DiscountName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<DiscountName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(description: "Discount name cannot be empty.");
        }

        return new DiscountName(name);
    }

    public static implicit operator string(DiscountName discountName) => discountName.Value;
}