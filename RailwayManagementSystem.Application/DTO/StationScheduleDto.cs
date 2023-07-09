namespace RailwayManagementSystem.Application.DTO;

public class StationScheduleDto
{
    public string StationName { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public int Platform { get; set; }
}