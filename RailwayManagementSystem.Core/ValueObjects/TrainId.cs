using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record TrainId
{
    public Guid Value { get; }

    public TrainId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static TrainId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(TrainId id)
        => id.Value;
    
    public static implicit operator TrainId(Guid value)
        => new(value);
}