using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Train
{
    public int Id { get; set; }
    public TrainName Name { get; set; }
    public int SeatsAmount { get; set; }
    public int CarrierId { get; set; }
    public Carrier Carrier { get; set; }
    public List<Seat> Seats { get; set; } = new();
    public List<Trip> Trips { get; set; } = new();

    private Train(TrainName name, int seatsAmount, Carrier carrier)
    {
        Name = name;
        SeatsAmount = seatsAmount;
        CarrierId = carrier.Id;
        Carrier = carrier;
    }

    public static Train Create(TrainName name, int seatsAmount, Carrier carrier)
    {
        return new Train(name, seatsAmount, carrier);
    }
    
    private Train() {}
}