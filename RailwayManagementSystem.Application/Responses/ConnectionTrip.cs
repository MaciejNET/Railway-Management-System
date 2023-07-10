namespace RailwayManagementSystem.Application.Responses;

public class ConnectionTrip
{
    public Guid TripId { get; set; }
    public string StartStation { get; set; }
    public string EndStation { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public decimal Price { get; set; }
    public TimeOnly TravelTime { get; set; }
}