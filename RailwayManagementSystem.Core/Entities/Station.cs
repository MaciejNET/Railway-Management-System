using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Station
{
    public StationId Id { get; private set; }
    public StationName Name { get; private set; }
    public City City { get; private set; }
    public int NumberOfPlatforms { get; private set; }

    private Station(StationId id, StationName name, City city, int numberOfPlatforms)
    {
        Id = id;
        Name = name;
        City = city;
        NumberOfPlatforms = numberOfPlatforms;
    }

    public static Station Create(StationId id, StationName name, City city, int numberOfPlatforms)
    {
        return new Station(id, name, city, numberOfPlatforms);
    }
    
    private Station() {}
}