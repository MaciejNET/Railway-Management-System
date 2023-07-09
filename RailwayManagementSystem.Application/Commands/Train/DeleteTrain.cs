using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Train;

public record DeleteTrain(Guid Id) : ICommand;