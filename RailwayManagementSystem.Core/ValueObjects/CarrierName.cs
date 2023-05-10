using ErrorOr;
using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record CarrierName
{
    public string Value { get; private set; }
    
    public CarrierName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidCarrierNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(CarrierName carrierName) => carrierName.Value;

    public static implicit operator CarrierName(string value) => new(value);
}