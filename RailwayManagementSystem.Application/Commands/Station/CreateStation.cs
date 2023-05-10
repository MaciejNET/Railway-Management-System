namespace RailwayManagementSystem.Application.Commands.Station;

public class CreateStation
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int NumberOfPlatforms { get; set; }
}