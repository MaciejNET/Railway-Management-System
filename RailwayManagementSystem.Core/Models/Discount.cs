using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Discount
{
    public int Id { get; set; }
    public Name Name { get; set; }
    public int Percentage { get; set; }

    [NotMapped] public virtual IEnumerable<Passenger> Passengers { get; set; }
}