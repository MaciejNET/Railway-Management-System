using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TripStationsCountException()
    : CustomException(message: "Trip must contains 2 or more stations in schedule.", httpStatusCode: 400);