namespace RailwayManagementSystem.Application.Commands.Passenger;

public class RegisterPassenger
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Age { get; set; }
    public string? DiscountName { get; set; } = string.Empty;
}