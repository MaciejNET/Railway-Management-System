namespace RailwayManagementSystem.Application.Commands.Train;

public class CreateTrain
{
    public string Name { get; set; } = string.Empty;
    public int SeatsAmount { get; set; }
    public string CarrierName { get; set; } = string.Empty;
}