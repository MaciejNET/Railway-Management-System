using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class InvalidScheduleStationsException : CustomException
{
    public InvalidScheduleStationsException() : base(message: "Trip schedule does not contains provided stations", httpStatusCode: 400)
    {
    }
}