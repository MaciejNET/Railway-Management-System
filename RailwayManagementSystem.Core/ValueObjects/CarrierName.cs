using ErrorOr;

namespace RailwayManagementSystem.Core.ValueObjects;

public record CarrierName
{
    private CarrierName(string value)
    {
        Value = value;
    }

    public string Value { get; private set; }

    public static ErrorOr<CarrierName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation(description: "Carrier name cannot be empty.");
        }

        return new CarrierName(name);
    }

    public static implicit operator string(CarrierName carrierName) => carrierName.Value;
}