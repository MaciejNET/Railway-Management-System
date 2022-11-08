using System.ComponentModel.DataAnnotations.Schema;

namespace RailwayManagementSystem.Core.Models;

public class Schedule
{
    public int Id { get; set; }

    [ForeignKey("Trip")] public int TripId { get; set; }

    public virtual Trip Trip { get; set; }

    [ForeignKey("Station")] public int StationId { get; set; }

    public virtual Station Station { get; set; }

    public TimeOnly ArrivalTime { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public int Platform { get; set; }
}