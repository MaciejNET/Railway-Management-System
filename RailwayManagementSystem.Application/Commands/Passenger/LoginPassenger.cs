using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Passenger;

public record LoginPassenger(string Email, string Password) : ICommand;