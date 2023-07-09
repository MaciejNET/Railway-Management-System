using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record StationScheduleId
{
    public Guid Value { get; }

    public StationScheduleId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static StationScheduleId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(StationScheduleId id)
        => id.Value;
    
    public static implicit operator StationScheduleId(Guid value)
        => new(value);
}