using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class DiscountNotFoundException : CustomException
{
    public int? Id { get; }
    public string? Name { get; }

    public DiscountNotFoundException(int id) : base(message: $"Discount with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
    
    public DiscountNotFoundException(string name) : base(message: $"Discount with name: {name} does not exists.", httpStatusCode: 404)
    {
        Name = name;
    }
}