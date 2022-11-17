using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class RailwayEmployee : User
{
    public Name Name { get; set; }
    public Name FirstName { get; set; }
    public Name LastName { get; set; }
}