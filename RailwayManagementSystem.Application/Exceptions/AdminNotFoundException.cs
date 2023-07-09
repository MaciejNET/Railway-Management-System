using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class AdminNotFoundException : CustomException
{
    public Guid? Id { get; }
    public string? Name { get; }

    public AdminNotFoundException(Guid id) : base(message: $"Admin with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
    
    public AdminNotFoundException(string name) : base(message: $"Admin with name: {name} does not exists.", httpStatusCode: 404)
    {
        Name = name;
    }
}