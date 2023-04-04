using System.ComponentModel.DataAnnotations.Schema;

namespace RailwayManagementSystem.Core.Models;

public class Schedule
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public Trip Trip { get; set; }
    public int StationId { get; set; }
    public Station Station { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public int Platform { get; set; }

    private Schedule(Trip trip, Station station, TimeOnly arrivalTime, TimeOnly departureTime, int platform)
    {
        TripId = trip.Id;
        Trip = trip;
        StationId = station.Id;
        Station = station;
        ArrivalTime = arrivalTime;
        DepartureTime = departureTime;
        Platform = platform;
    }

    public static Schedule Create(Trip trip, Station station, TimeOnly arrivalTime, TimeOnly departureTime, int platform)
    {
        return new Schedule(trip, station, arrivalTime, departureTime, platform);
    }
    
    private Schedule() {}
}