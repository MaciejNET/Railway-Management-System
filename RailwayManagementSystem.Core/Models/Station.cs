using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Station
{
    public int Id { get; set; }
    public StationName Name { get; set; }
    public City City { get; set; }
    public int NumberOfPlatforms { get; set; }
    public List<Schedule> Schedule { get; set; } = new();
    public List<Ticket> Tickets { get; set; } = new();

    private Station(StationName name, City city, int numberOfPlatforms)
    {
        Name = name;
        City = city;
        NumberOfPlatforms = numberOfPlatforms;
    }

    public static Station Create(StationName name, City city, int numberOfPlatforms)
    {
        return new Station(name, city, numberOfPlatforms);
    }
    
    private Station() {}
}