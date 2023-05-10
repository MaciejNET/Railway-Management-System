namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidStationNameException : CustomException
{
    public InvalidStationNameException() : base(message: "Station name is invalid.", httpStatusCode: 400)
    {
    }
}