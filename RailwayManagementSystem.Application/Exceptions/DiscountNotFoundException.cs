using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class DiscountNotFoundException : CustomException
{
    public Guid? Id { get; }
    public string? Name { get; }

    public DiscountNotFoundException(Guid id) : base(message: $"Discount with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
    
    public DiscountNotFoundException(string name) : base(message: $"Discount with name: {name} does not exists.", httpStatusCode: 404)
    {
        Name = name;
    }
}