namespace RailwayManagementSystem.Infrastructure.Commands.Passenger;

public class UpdatePassenger
{
    public int? Id { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
}