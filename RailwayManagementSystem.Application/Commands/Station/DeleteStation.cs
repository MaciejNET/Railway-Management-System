using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Station;

public record DeleteStation(Guid Id) : ICommand;