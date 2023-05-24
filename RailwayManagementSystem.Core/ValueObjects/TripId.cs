using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record TripId
{
    public Guid Value { get; }

    public TripId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static TripId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(TripId id)
        => id.Value;
    
    public static implicit operator TripId(Guid value)
        => new(value);
}