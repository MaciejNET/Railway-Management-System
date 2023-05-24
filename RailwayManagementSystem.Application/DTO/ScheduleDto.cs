namespace RailwayManagementSystem.Application.DTO;

public class ScheduleDto
{
    public string StationName { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime DepartureTime { get; set; }
    public int Platform { get; set; }
}