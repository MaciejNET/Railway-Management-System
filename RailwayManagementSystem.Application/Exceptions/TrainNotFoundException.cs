using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class TrainNotFoundException : CustomException
{
    public int? Id { get; }
    public string? Name { get; }

    public TrainNotFoundException(int id) : base(message: $"Train with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
    
    public TrainNotFoundException(string name) : base(message: $"Train with name: {name} does not exists.", httpStatusCode: 404)
    {
        Name = name;
    }
}