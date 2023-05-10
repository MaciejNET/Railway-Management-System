namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidRailwayEmployeeNameException : CustomException
{
    public InvalidRailwayEmployeeNameException() : base(message: "Railway employee name is invalid.", httpStatusCode: 400)
    {
    }
}