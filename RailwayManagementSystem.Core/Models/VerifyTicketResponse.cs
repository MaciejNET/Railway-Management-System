namespace RailwayManagementSystem.Core.Models;

public class VerifyTicketResponse
{
    public bool IsValid { get; set; }
    public string StartStation { get; set; } = string.Empty;
    public string EndStation { get; set; } = string.Empty;
}