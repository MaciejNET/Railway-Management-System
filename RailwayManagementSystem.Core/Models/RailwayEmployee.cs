using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class RailwayEmployee : User
{
    public RailwayEmployeeName Name { get; set; }
    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }

    private RailwayEmployee(RailwayEmployeeName name, FirstName firstName, LastName lastName, byte[] passwordHash, byte[] passwordSalt, Role role) : base(passwordHash, passwordSalt, role)
    {
        Name = name;
        FirstName = firstName;
        LastName = lastName;
    }

    public static RailwayEmployee Create(RailwayEmployeeName name, FirstName firstName, LastName lastName,
        byte[] passwordHash, byte[] passwordSalt)
    {
        return new RailwayEmployee(name, firstName, lastName, passwordHash, passwordSalt, Role.RailwayEmployee);
    }
    
    private RailwayEmployee() {}
}