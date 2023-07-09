using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TripStationsCountException : CustomException
{
    public TripStationsCountException() : base(message: "Trip must contains 2 or more stations in schedule.", httpStatusCode: 400)
    {
    }
}