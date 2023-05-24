using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Train;

public record CreateTrain(Guid Id, string Name, int SeatsAmount, string CarrierName) : ICommand;