using ErrorOr;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record TrainName
{
    public string Value { get; private set; }
    
    public TrainName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidTrainNameException();
        }

        Value = value;
    }

    public static implicit operator string(TrainName trainName) => trainName.Value;

    public static implicit operator TrainName(string value) => new(value);
}