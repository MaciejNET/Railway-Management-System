using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TrainInUseException : CustomException
{
    public TrainInUseException() : base(message: "The train is in use.", httpStatusCode: 400)
    {
    }
}