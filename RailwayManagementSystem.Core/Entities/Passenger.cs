using RailwayManagementSystem.Core.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Passenger : User
{
    private readonly List<Ticket> _tickets = new();
    
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public DateOfBirth DateOfBirth { get; private set; }
    public Discount? Discount { get; private set; }
    public IReadOnlyList<Ticket> Tickets => _tickets.AsReadOnly();

    private Passenger(UserId id, FirstName firstName, LastName lastName, Email email, DateOfBirth dateOfBirth, Discount? discount, Password password, Role role)
        : base(id, password, role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DateOfBirth = dateOfBirth;
        Discount = discount;
    }
    
    private Passenger(UserId id, FirstName firstName, LastName lastName, Email email, DateOfBirth dateOfBirth, Password password, Role role)
        : base(id, password, role)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        DateOfBirth = dateOfBirth;
    }

    public static Passenger Create(UserId id, FirstName firstName, LastName lastName, Email email,
        DateOfBirth dateOfBirth, Password password)
    {
        return new Passenger(id, firstName, lastName, email, dateOfBirth, password, Role.Passenger);
    }
    
    public static Passenger CreateWithDiscount(UserId id, FirstName firstName, LastName lastName, Email email,
         DateOfBirth dateOfBirth, Discount discount, Password password)
    {
        return new Passenger(id, firstName, lastName, email, dateOfBirth, discount, password, Role.Passenger);
    }

    public void UpdateEmail(Email email)
    {
        Email = email;
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