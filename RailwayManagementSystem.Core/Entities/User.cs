using RailwayManagementSystem.Core.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public abstract class User
{
    public UserId Id { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }

    protected User(UserId id, Password password, Role role)
    {
        Id = id;
        Password = password;
        Role = role;
    }
    
    protected User() {}
}