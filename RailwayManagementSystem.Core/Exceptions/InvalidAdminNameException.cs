namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidAdminNameException : CustomException
{
    public InvalidAdminNameException() : base(message: "Admin name is invalid.", httpStatusCode: 400)
    {
    }
}