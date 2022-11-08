namespace RailwayManagementSystem.Infrastructure.DTOs;

public class ScheduleDto
{
    public int Id { get; set; }
    public string StationName { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public int Platform { get; set; }
}