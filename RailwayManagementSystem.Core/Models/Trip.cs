using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RailwayManagementSystem.Core.Models;

public class Trip
{
    public int Id { get; set; }
    
    [Precision(5, 2)]
    public decimal Price { get; set; }

    [ForeignKey("Train")] public int TrainId { get; set; }

    public Train Train { get; set; }

    [NotMapped] public IEnumerable<Schedule> Schedules { get; set; }

    [ForeignKey("TripInterval")] public int TripIntervalId { get; set; }

    public virtual TripInterval TripInterval { get; set; }

    [NotMapped] public virtual IEnumerable<Ticket> Tickets { get; set; }
}