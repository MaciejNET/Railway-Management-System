namespace RailwayManagementSystem.Infrastructure.DTOs;

public class ConnectionTripDto
{
    public string TrainName { get; set; } = string.Empty;
    public string StartStation { get; set; } = string.Empty;
    public string EndStation { get; set; } = string.Empty;
    public TimeOnly DepartureTime { get; set; }
    public TimeOnly ArrivalTime { get; set; }
}