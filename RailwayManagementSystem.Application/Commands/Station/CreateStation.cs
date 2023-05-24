using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Station;

public record CreateStation(Guid Id, string Name, string City, int NumberOfPlatforms) : ICommand;