using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Schedule
{
    public ScheduleId Id { get; private set; }
    public TripId TripId { get; private set; }
    public Station Station { get; private set; }
    public DateTime ArrivalTime { get; private set; }
    public DateTime DepartureTime { get; private set; }
    public int Platform { get; private set; }

    private Schedule(TripId tripId, Station station, DateTime arrivalTime, DateTime departureTime, int platform)
    {
        Id = ScheduleId.Create();
        TripId = tripId;
        Station = station;
        ArrivalTime = arrivalTime;
        DepartureTime = departureTime;
        Platform = platform;
    }

    public static Schedule Create(TripId tripId, Station station, DateTime arrivalTime, DateTime departureTime, int platform)
    {
        return new Schedule(tripId, station, arrivalTime, departureTime, platform);
    }
    
    private Schedule() {}
}