using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class AdminAlreadyExistsException(string adminName)
    : CustomException(message: $"Admin with name: {adminName} already exists.", httpStatusCode: 400)
{
    public string AdminName { get; } = adminName;
}