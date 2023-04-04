using RailwayManagementSystem.Core.Models.Enums;

namespace RailwayManagementSystem.Core.Models;

public abstract class User
{
    public int Id { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public Role Role { get; set; }

    protected User(byte[] passwordHash, byte[] passwordSalt, Role role)
    {
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Role = role;
    }
    
    protected User() {}
}