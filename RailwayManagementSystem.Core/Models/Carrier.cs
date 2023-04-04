using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Carrier
{
    public int Id { get; set; }
    public CarrierName Name { get; set; }

    public List<Train> Trains { get; set; } = new();

    private Carrier(CarrierName name)
    {
        Name = name;
    }

    public static Carrier Create(CarrierName name)
    {
        return new Carrier(name);
    }
    
    private Carrier() {}
}