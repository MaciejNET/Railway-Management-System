namespace RailwayManagementSystem.Application.DTO;

public class PassengerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string? Discount { get; set; }
}