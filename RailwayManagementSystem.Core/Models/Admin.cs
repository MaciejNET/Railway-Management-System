using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Admin : User
{
    public Name Name { get; set; }
}