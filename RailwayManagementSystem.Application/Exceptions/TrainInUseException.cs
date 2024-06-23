using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TrainInUseException() : CustomException(message: "The train is in use.", httpStatusCode: 400);