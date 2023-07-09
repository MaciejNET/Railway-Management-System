using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Carrier;

public record DeleteCarrier(Guid Id) : ICommand;