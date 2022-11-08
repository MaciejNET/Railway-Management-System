namespace RailwayManagementSystem.Infrastructure.DTOs;

public class ConnectionTripDto
{
    public string TrainName { get; set; }
    public string StartStation { get; set; }
    public string EndStation { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public TimeOnly ArrivalTime { get; set; }
}