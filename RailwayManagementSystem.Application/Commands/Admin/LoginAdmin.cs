using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Admin;

public record LoginAdmin(string Name, string Password) : ICommand;