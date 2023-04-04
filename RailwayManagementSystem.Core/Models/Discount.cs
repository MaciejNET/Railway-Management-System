using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Discount
{
    public int Id { get; set; }
    public DiscountName Name { get; set; }
    public int Percentage { get; set; }
    public List<Passenger> Passengers { get; set; } = new();

    private Discount(DiscountName name, int percentage)
    {
        Name = name;
        Percentage = percentage;
    }

    public static Discount Create(DiscountName name, int percentage)
    {
        return new Discount(name, percentage);
    }
    
    private Discount() {}
}