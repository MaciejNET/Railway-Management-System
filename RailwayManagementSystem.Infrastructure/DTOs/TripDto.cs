namespace RailwayManagementSystem.Infrastructure.DTOs;

public class TripDto
{
    public int Id { get; set; }
    public string TrainName { get; set; }
    public string StartStation { get; set; }
    public string EndStation { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public TripIntervalDto TripIntervalDto { get; set; }
}