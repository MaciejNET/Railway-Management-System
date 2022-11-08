using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Station
{
    public int Id { get; set; }
    public Name Name { get; set; }
    public City City { get; set; }
    public int NumberOfPlatforms { get; set; }

    [NotMapped] public virtual IEnumerable<Schedule> Schedule { get; set; }

    [NotMapped] public virtual IEnumerable<Ticket> Tickets { get; set; }
}