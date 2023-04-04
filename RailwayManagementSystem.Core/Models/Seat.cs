using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.Models.Enums;

namespace RailwayManagementSystem.Core.Models;

public class Seat
{
    public int Id { get; set; }
    public int SeatNumber { get; set; }
    public Place Place { get; set; }
    public int TrainId { get; set; }
    public Train Train { get; set; }
    public List<Ticket> Ticket { get; set; } = new();

    private Seat(int seatNumber, Place place, Train train)
    {
        SeatNumber = seatNumber;
        Place = place;
        TrainId = train.Id;
        Train = train;
    }

    public static Seat Create(int seatNumber, Place place, Train train)
    {
        return new Seat(seatNumber, place, train);
    }
    
    private Seat() {}
}