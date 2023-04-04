namespace RailwayManagementSystem.Core.Models;

public class Ticket
{
    public int Id { get; set; }
    public int TripId { get; set; }
    public Trip Trip { get; set; }
    public int PassengerId { get; set; }
    public Passenger Passenger { get; set; }
    public decimal Price { get; set; }
    public int SeatId { get; set; }
    public Seat Seat { get; set; }
    public DateOnly TripDate { get; set; }
    public List<Station> Stations { get; set; } = new();

    private Ticket(Trip trip, Passenger passenger, decimal price, Seat seat, DateOnly tripDate, List<Station> stations)
    {
        TripId = trip.Id;
        Trip = trip;
        PassengerId = passenger.Id;
        Passenger = passenger;
        Price = price;
        SeatId = seat.Id;
        Seat = seat;
        TripDate = tripDate;
        Stations = stations;
    }

    public static Ticket Create(Trip trip, Passenger passenger, decimal price, Seat seat, DateOnly tripDate, List<Station> stations)
    {
        return new Ticket(trip, passenger, price, seat, tripDate, stations);
    }
    
    private Ticket() {}
}