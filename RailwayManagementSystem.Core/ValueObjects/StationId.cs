using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record StationId
{
    public Guid Value { get; }

    public StationId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static StationId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(StationId id)
        => id.Value;
    
    public static implicit operator StationId(Guid value)
        => new(value);
}