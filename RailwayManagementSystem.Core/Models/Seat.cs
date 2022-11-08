using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.Models.Enums;

namespace RailwayManagementSystem.Core.Models;

public class Seat
{
    public int Id { get; set; }
    public int SeatNumber { get; set; }
    public Place Place { get; set; }

    [ForeignKey("Train")] public int TrainId { get; set; }

    public virtual Train Train { get; set; }

    [NotMapped] public virtual IEnumerable<Ticket> Ticket { get; set; }
}