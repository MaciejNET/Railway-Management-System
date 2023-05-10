using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class AdminAlreadyExistsException : CustomException
{
    public string AdminName { get; }

    public AdminAlreadyExistsException(string adminName) : base(message: $"Admin with name: {adminName} already exists.", httpStatusCode: 400)
    {
        AdminName = adminName;
    }
}