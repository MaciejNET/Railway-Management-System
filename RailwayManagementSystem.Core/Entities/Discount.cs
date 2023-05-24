using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Discount
{
    public DiscountId Id { get; private set; }
    public DiscountName Name { get; private set; }
    public int Percentage { get; private set; }

    private Discount(DiscountId id, DiscountName name, int percentage)
    {
        Id = id;
        Name = name;
        Percentage = percentage;
    }

    public static Discount Create(DiscountId id, DiscountName name, int percentage)
    {
        return new Discount(id, name, percentage);
    }
    
    private Discount() {}
}