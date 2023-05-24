using RailwayManagementSystem.Core.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Train
{
    private readonly List<Seat> _seats = new();
    private readonly List<Trip> _trips = new();
    
    public TrainId Id { get; private set; }
    public TrainName Name { get; private set; }
    public int SeatsAmount { get; private set; }
    public Carrier Carrier { get; private set; }
    public IReadOnlyList<Seat> Seats => _seats.AsReadOnly();
    public IReadOnlyList<Trip> Trips => _trips.AsReadOnly(); 

    private Train(TrainId id, TrainName name, int seatsAmount, Carrier carrier)
    {
        Id = id;
        Name = name;
        SeatsAmount = seatsAmount;
        Carrier = carrier;
        _seats = GenerateSeats(seatsAmount);
    }

    public static Train Create(TrainId id, TrainName name, int seatsAmount, Carrier carrier)
    {
        return new Train(id, name, seatsAmount, carrier);
    }

    public void AddTrip(Trip trip)
    {
        _trips.Add(trip);
    }

    private List<Seat> GenerateSeats(int seatsAmount)
    {
        List<Seat> seats = new();
        for (var i = 1; i <= seatsAmount; i++)
        {
            var seat = new Seat(i, i % 2 == 0 ? Place.Middle : Place.Window, Id);
            seats.Add(seat);
        }

        return seats;
    }

    private Train() {}
}