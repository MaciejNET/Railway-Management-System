namespace RailwayManagementSystem.Core.Exceptions;

public class TrainDoesNotRunOnGivenDateException : CustomException
{
    public string TrainName { get; }
    public DateTime Date { get; }

    public TrainDoesNotRunOnGivenDateException(string trainName, DateTime date) : base(message: $"Train with name: {trainName} does not run on {date:d}", httpStatusCode: 400)
    {
        TrainName = trainName;
        Date = date;
    }
}