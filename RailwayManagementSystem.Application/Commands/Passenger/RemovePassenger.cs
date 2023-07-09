using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Passenger;

public record RemovePassenger(Guid Id) : ICommand;