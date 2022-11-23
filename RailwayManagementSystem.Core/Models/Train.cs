using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Train
{
    public int Id { get; set; }
    public Name Name { get; set; }
    public int SeatsAmount { get; set; }

    [ForeignKey("Carrier")] public int CarrierId { get; set; }

    public virtual Carrier Carrier { get; set; }

    [NotMapped] public virtual IEnumerable<Seat> Seats { get; set; }

    [NotMapped] public virtual IEnumerable<Trip> Trips { get; set; }
}