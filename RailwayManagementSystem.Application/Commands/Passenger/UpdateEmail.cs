using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Passenger;

public record UpdateEmail(Guid Id, string Email) : ICommand;