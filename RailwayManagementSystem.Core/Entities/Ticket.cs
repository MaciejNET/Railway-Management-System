using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Ticket
{
    private readonly List<Station> _stations = [];
    
    public TicketId Id { get; private set; }
    public Trip Trip { get; private set; }
    public UserId PassengerId { get; private set; }
    public decimal Price { get; private set; }
    public Seat Seat { get; private set; }
    public DateTime TripDate { get; private set; }
    public IReadOnlyList<Station> Stations => _stations.AsReadOnly();

    internal Ticket(Trip trip, UserId passengerId, decimal price, Seat seat, DateTime tripDate, List<Station> stations)
    {
        Id = TicketId.Create();
        Trip = trip;
        PassengerId = passengerId;
        Price = price;
        Seat = seat;
        TripDate = tripDate;
        _stations = stations;
    }

    private Ticket() {}
}