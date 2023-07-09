using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Discount;

public record DeleteDiscount(Guid Id) : ICommand;