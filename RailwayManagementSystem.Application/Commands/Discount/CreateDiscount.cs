using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Discount;

public record CreateDiscount(Guid Id, string Name, int Percentage) : ICommand;