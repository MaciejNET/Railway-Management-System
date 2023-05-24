namespace RailwayManagementSystem.Core.ValueObjects;

public record TripInterval(
    bool Monday,
    bool Tuesday,
    bool Wednesday,
    bool Thursday,
    bool Friday,
    bool Saturday,
    bool Sunday)
{
    public bool IsTripRunOnGivenDate(DayOfWeek dayOfWeek)
        => dayOfWeek switch
        {
            DayOfWeek.Monday => Monday,
            DayOfWeek.Tuesday => Tuesday,
            DayOfWeek.Wednesday => Wednesday,
            DayOfWeek.Thursday => Thursday,
            DayOfWeek.Friday => Friday,
            DayOfWeek.Saturday => Saturday,
            DayOfWeek.Sunday => Sunday,
            _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null)
        };
}