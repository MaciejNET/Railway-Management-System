using RailwayManagementSystem.Core.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Passenger : User
{
    private readonly List<Ticket> _tickets = new();
    
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public int Age { get; private set; }
    public Discount? Discount { get; private set; }
    public IReadOnlyList<Ticket> Tickets => _tickets.AsReadOnly();

    private Passenger(UserId id, FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber, int age, Discount? discount, Password password, Role role)
        : base(id, password, role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Age = age;
        Discount = discount;
    }

    public static Passenger Create(UserId id, FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber,
        int age, Password password)
    {
        return new Passenger(id, firstName, lastName, email, phoneNumber, age, null, password, Role.Passenger);
    }
    
    public static Passenger CreateWithDiscount(UserId id, FirstName firstName, LastName lastName, Email email, PhoneNumber phoneNumber,
        int age, Discount discount, Password password)
    {
        return new Passenger(id, firstName, lastName, email, phoneNumber, age, discount, password, Role.Passenger);
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = phoneNumber;
    }

    public void UpdateDiscount(Discount discount)
    {
        Discount = discount;
    }

    public void AddTicket(Ticket ticket)
    {
        _tickets.Add(ticket);
    }
    
    private Passenger() {}
}