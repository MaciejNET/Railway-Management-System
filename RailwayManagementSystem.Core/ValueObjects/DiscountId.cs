using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record DiscountId
{
    public Guid Value { get; }

    public DiscountId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static DiscountId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(DiscountId id)
        => id.Value;
    
    public static implicit operator DiscountId(Guid value)
        => new(value);
}