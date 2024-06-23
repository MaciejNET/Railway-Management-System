using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

internal sealed class StationScheduleExistsException()
    : CustomException(message: "There is already a schedule in this station.", httpStatusCode: 400);