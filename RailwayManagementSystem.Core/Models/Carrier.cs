using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Carrier
{
    public int Id { get; set; }
    public Name Name { get; set; }

    [NotMapped] public virtual IEnumerable<Train> Trains { get; set; }
}