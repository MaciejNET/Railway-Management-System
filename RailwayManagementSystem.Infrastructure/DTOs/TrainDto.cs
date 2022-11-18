namespace RailwayManagementSystem.Infrastructure.DTOs;

public class TrainDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SeatsAmount { get; set; }
    public string CarrierName { get; set; } = string.Empty;
}