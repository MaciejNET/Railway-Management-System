namespace RailwayManagementSystem.Application.Commands.Trip;

public class ConnectionTrip
{
    public string StartStation { get; set; } = string.Empty;
    public string EndStation { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}