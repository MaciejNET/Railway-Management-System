using System.ComponentModel.DataAnnotations.Schema;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Passenger : User
{
    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }
    public Email Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public int Age { get; set; }
    public int? DiscountId { get; set; }
    public Discount? Discount { get; set; }
    public List<Ticket> Tickets { get; set; } = new();

    private Passenger(FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, int age, Discount? discount, byte[] passwordHash, byte[] passwordSalt, Role role)
        : base(passwordHash, passwordSalt, role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Age = age;
        DiscountId = discount.Id;
        Discount = discount;
    }

    public static Passenger Create(FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber,
        int age, Discount? discount, byte[] passwordHash, byte[] passwordSalt)
    {
        return new Passenger(firstName, lastName, email, phoneNumber, age, discount, passwordHash, passwordSalt, Role.Passenger);
    }
    
    private Passenger() {}
}