using RailwayManagementSystem.Core.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class RailwayEmployee : User
{
    public RailwayEmployeeName Name { get; private set; }
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }

    private RailwayEmployee(UserId id, RailwayEmployeeName name, FirstName firstName, LastName lastName, Password password, Role role) : base(id, password, role)
    {
        Name = name;
        FirstName = firstName;
        LastName = lastName;
    }

    public static RailwayEmployee Create(UserId id, RailwayEmployeeName name, FirstName firstName, LastName lastName, Password password)
    {
        return new RailwayEmployee(id, name, firstName, lastName, password, Role.RailwayEmployee);
    }
    
    private RailwayEmployee() {}
}