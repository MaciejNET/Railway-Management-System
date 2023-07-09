using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Trip;

public record DeleteTrip(Guid Id) : ICommand;