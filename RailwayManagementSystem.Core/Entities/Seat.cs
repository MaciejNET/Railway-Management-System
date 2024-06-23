using RailwayManagementSystem.Core.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Seat
{
    private readonly List<Ticket> _tickets = [];
    
    public SeatId Id { get; private set; }
    public int SeatNumber { get; private set; }
    public Place Place { get; private set; }
    public TrainId TrainId { get; private set; }
    public IReadOnlyList<Ticket> Tickets => _tickets.AsReadOnly();

    internal Seat(int seatNumber, Place place, TrainId trainId)
    {
        Id = SeatId.Create();
        SeatNumber = seatNumber;
        Place = place;
        TrainId = trainId;
    }

    internal void AddTicket(Ticket ticket)
    {
        _tickets.Add(ticket);
    }

    private Seat() {}
}