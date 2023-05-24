using RailwayManagementSystem.Core.Exceptions;

namespace RailwayManagementSystem.Application.Exceptions;

public sealed class StationNotFoundException : CustomException
{
    public Guid? Id { get; }
    public string? StationName { get; }

    public StationNotFoundException(Guid id) : base(message: $"Station with Id: {id} does not exists.", httpStatusCode: 404)
    {
        Id = id;
    }
    
    public StationNotFoundException(string stationName) : base(message: $"Station with name: {stationName} does not exists.", httpStatusCode: 404)
    {
        StationName = stationName;
    }
}