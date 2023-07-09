using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Ticket;

public record CancelTicket(Guid Id, Guid PassengerId) : ICommand;