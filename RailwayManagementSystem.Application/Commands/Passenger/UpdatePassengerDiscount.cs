using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Passenger;

public record UpdatePassengerDiscount(Guid PassengerId, string DiscountName) : ICommand;