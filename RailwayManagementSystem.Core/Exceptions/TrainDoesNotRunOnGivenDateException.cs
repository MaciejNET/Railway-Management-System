namespace RailwayManagementSystem.Core.Exceptions;

public class TrainDoesNotRunOnGivenDateException(string trainName, DateTime date)
    : CustomException(message: $"Train with name: {trainName} does not run on {date:d}", httpStatusCode: 400)
{
    public string TrainName { get; } = trainName;
    public DateTime Date { get; } = date;
}