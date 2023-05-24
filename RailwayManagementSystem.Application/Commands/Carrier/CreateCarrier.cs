using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Carrier;

public record CreateCarrier(Guid Id, string Name) : ICommand;