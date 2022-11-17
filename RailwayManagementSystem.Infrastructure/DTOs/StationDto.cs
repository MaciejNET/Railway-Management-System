namespace RailwayManagementSystem.Infrastructure.DTOs;

public class StationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int NumberOfPlatforms { get; set; }
}