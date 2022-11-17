namespace RailwayManagementSystem.Infrastructure.DTOs;

public class TripDto
{
    public int Id { get; set; }
    public string TrainName { get; set; } = string.Empty;
    public string StartStation { get; set; } = string.Empty;
    public string EndStation { get; set; } = string.Empty;
    public TimeOnly ArrivalTime { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public TripIntervalDto TripIntervalDto { get; set; }
}