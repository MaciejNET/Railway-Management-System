namespace RailwayManagementSystem.Core.Exceptions;

public sealed class InvalidStationOrderException : CustomException
{
    public InvalidStationOrderException() : base(message: "Invalid order of start and end stations for the trip.", httpStatusCode: 400)
    {
    }
}