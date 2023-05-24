using RailwayManagementSystem.Core.Enums;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class Admin : User
{
    public AdminName Name { get; set; }

    private Admin(UserId id, AdminName name, Password password, Role role) : base(id, password, role)
    {
        Name = name;
    }

    public static Admin Create(UserId id, AdminName name, Password password)
    {
        return new Admin(id, name, password, Role.Admin);
    }

    private Admin() {}
}