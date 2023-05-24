using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record UserId
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static UserId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(UserId id)
        => id.Value;
    
    public static implicit operator UserId(Guid value)
        => new(value);
}