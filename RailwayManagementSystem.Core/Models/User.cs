using RailwayManagementSystem.Core.Models.Enums;

namespace RailwayManagementSystem.Core.Models;

public abstract class User
{
    public int Id { get; set; }
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];
    public Role Role { get; set; }
}