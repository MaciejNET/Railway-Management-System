namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidTrainNameException : CustomException
{
    public InvalidTrainNameException() : base(message: "Train name is invalid.", httpStatusCode: 400)
    {
    }
}