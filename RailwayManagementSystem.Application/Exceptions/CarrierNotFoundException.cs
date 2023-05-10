using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class CarrierNotFoundException : CustomException
{
    public int? Id { get; }
    public string? Name { get; }

    public CarrierNotFoundException(int id) : base(message: $"Carrier with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
    
    public CarrierNotFoundException(string name) : base(message: $"Carrier with name: {name} does not exists.", httpStatusCode: 404)
    {
        Name = name;
    }
}