using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record ValidDate
{
    public DateOnly From { get; private set; }
    public DateOnly To { get; private set; }

    public ValidDate(DateOnly from, DateOnly to)
    {
        if (from > to)
        {
            throw new InvalidDateRangeException();
        }

        From = from;
        To = to;
    }

    public bool IsTripRunningOnGivenDate(DateOnly date) => date >= From && date <= To;
}