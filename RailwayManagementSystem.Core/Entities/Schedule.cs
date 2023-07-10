using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Schedule
{
    private readonly List<StationSchedule> _stations = new();

    public ScheduleId Id { get; private set; }
    public TripId TripId { get; private set; }  
    public ValidDate ValidDate { get; private set; }
    public TripAvailability TripAvailability { get; private set; }
    public IReadOnlyList<StationSchedule> Stations => _stations.AsReadOnly();

    private Schedule(TripId tripId, ValidDate validDate, TripAvailability tripAvailability)
    {
        Id = ScheduleId.Create();
        TripId = tripId;
        ValidDate = validDate;
        TripAvailability = tripAvailability;
    }

    public static Schedule Create(TripId tripId, ValidDate validDate, TripAvailability tripAvailability)
    {
        return new Schedule(tripId, validDate, tripAvailability);
    }

    public void AddStationSchedule(StationSchedule stationSchedule)
    {
        if (Stations.Any(x => stationSchedule.DepartureTime < x.ArrivalTime))
        {
            throw new InvalidNewStationDepartureTimeException();
        }

        var index = 0;
        while (index < _stations.Count && stationSchedule.DepartureTime > _stations[index].DepartureTime)
        {
            index++;
        }
        
        _stations.Insert(index, stationSchedule);
    }

    public bool IsTripRunningOnGivenDate(DateTime date)
    {
        var dateOnly = DateOnly.FromDateTime(date);
        var dayOfWeek = date.DayOfWeek;

        return ValidDate.IsTripRunningOnGivenDate(dateOnly) && TripAvailability.IsTripRunningOnGivenDate(dayOfWeek);
    }
    
    internal int GetStationIndex(StationSchedule stationSchedule)
    {
        var index = _stations.IndexOf(stationSchedule);
        
        if (index == -1)
        {
            throw new StationDoesNotExistOnScheduleException();
        }

        return index;
    }

    private Schedule() {}
}