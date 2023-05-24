using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record SeatId
{
    public Guid Value { get; }

    public SeatId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static SeatId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(SeatId id)
        => id.Value;
    
    public static implicit operator SeatId(Guid value)
        => new(value);
}