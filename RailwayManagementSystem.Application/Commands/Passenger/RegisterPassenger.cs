using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Passenger;

public record RegisterPassenger(Guid Id, string FirstName, string LastName, string Email, string PhoneNumber, string Password, int Age, string? DiscountName) : ICommand;