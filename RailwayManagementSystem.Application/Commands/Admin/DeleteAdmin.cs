using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Admin;

public record DeleteAdmin(Guid AdminId) : ICommand;