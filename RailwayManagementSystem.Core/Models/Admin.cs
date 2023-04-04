using ErrorOr;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Models;

public class Admin : User
{
    public AdminName Name { get; set; }

    private Admin(AdminName name, byte[] passwordHash, byte[] passwordSalt, Role role) : base(passwordHash, passwordSalt, role)
    {
        Name = name;
    }

    public static Admin Create(AdminName name, byte[] passwordHash, byte[] passwordSalt)
    {
        return new Admin(name, passwordHash, passwordSalt, Role.Admin);
    }

    private Admin() {}
}