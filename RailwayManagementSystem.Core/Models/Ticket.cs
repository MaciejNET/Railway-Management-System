using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RailwayManagementSystem.Core.Models;

public class Ticket
{
    public int Id { get; set; }

    [ForeignKey("Trip")] public int TripId { get; set; }

    public virtual Trip Trip { get; set; }

    [ForeignKey("Passenger")] public int PassengerId { get; set; }

    public virtual Passenger Passenger { get; set; }

    [Precision(8, 2)]
    public double Price { get; set; }

    [ForeignKey("Seat")] public int SeatId { get; set; }

    public virtual Seat Seat { get; set; }

    public DateOnly TripDate { get; set; }

    [NotMapped] public IEnumerable<Station> Stations { get; set; }
}