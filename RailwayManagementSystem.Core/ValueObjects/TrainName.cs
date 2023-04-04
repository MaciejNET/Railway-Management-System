using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record TrainName
{
    private TrainName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<TrainName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(description: "Train name cannot be empty.");
        }

        return new TrainName(name);
    }

    public static implicit operator string(TrainName trainName) => trainName.Value;
}