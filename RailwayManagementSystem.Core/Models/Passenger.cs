using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Passenger
{
    public int Id { get; set; }
    public Name FirstName { get; set; }
    public Name LastName { get; set; }
    public Email Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];
    public int Age { get; set; }

    [ForeignKey("Discount")] public int? DiscountId { get; set; }

    public virtual Discount? Discount { get; set; }

    [NotMapped] public virtual IEnumerable<Ticket> Tickets { get; set; }
}