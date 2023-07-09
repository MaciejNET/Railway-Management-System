using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Schedule
{
    public ScheduleId Id { get; private set; }
    public TripId TripId { get; private set; }  
    public ValidDate ValidDate { get; private set; }
    public TripAvailability TripAvailability { get; private set; }
    public IReadOnlyList<StationSchedule> Stations { get; private set; }

    private Schedule(TripId tripId, ValidDate validDate, TripAvailability tripAvailability, List<StationSchedule> stations)
    {
        Id = ScheduleId.Create();
        TripId = tripId;
        ValidDate = validDate;
        TripAvailability = tripAvailability;
        Stations = stations.AsReadOnly();
    }

    public static Schedule Create(TripId tripId, ValidDate validDate, TripAvailability tripAvailability, List<StationSchedule> stations)
    {
        return new Schedule(tripId, validDate, tripAvailability, stations);
    }

    public bool IsTripRunningOnGivenDate(DateTime date)
    {
        var dateOnly = DateOnly.FromDateTime(date);
        var dayOfWeek = date.DayOfWeek;

        return ValidDate.IsTripRunningOnGivenDate(dateOnly) && TripAvailability.IsTripRunningOnGivenDate(dayOfWeek);
    }

    private Schedule() {}
}