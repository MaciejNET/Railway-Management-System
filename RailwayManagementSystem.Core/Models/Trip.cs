using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Trip
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public int TrainId { get; set; }
    public Train Train { get; set; }
    public List<Schedule> Schedules { get; set; } = new();
    public TripInterval TripInterval { get; set; }
    public List<Ticket> Tickets { get; set; } = new();

    private Trip(decimal price, Train train, TripInterval tripInterval)
    {
        Price = price;
        TrainId = train.Id;
        Train = train;
        TripInterval = tripInterval;
    }

    public static Trip Create(decimal price, Train train, TripInterval tripInterval)
    {
        return new Trip(price, train, tripInterval);
    }
    
    private Trip() {}
}