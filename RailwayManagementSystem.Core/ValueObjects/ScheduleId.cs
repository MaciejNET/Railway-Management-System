using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record ScheduleId
{
    public Guid Value { get; }

    public ScheduleId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static ScheduleId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ScheduleId id)
        => id.Value;
    
    public static implicit operator ScheduleId(Guid value)
        => new(value);
}