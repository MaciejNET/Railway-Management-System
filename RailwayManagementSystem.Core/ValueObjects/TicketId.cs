using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record TicketId
{
    public Guid Value { get; }

    public TicketId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }

        Value = value;
    }

    public static TicketId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(TicketId id)
        => id.Value;
    
    public static implicit operator TicketId(Guid value)
        => new(value);
}