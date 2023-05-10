namespace RailwayManagementSystem.Application.DTOs;

public class PassengerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Age { get; set; }
    public string? DiscountName { get; set; }
}