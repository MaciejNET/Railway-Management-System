using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Core.ValueObjects;

public record AdminName
{
    public string Value { get; private set; }
    
    public AdminName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidAdminNameException();
        }
        
        Value = value;
    }

    public static implicit operator string(AdminName adminName) => adminName.Value;

    public static implicit operator AdminName(string value) => new(value);
}