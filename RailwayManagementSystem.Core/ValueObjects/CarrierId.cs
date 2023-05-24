using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record CarrierId
{
    public Guid Value { get; }

    public CarrierId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static CarrierId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(CarrierId id)
        => id.Value;
    
    public static implicit operator CarrierId(Guid value)
        => new(value);
}