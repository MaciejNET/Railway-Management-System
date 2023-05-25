using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Admin;

public record CreateAdmin(Guid Id, string Name, string Password) : ICommand;