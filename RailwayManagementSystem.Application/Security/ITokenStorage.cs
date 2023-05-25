using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Security;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}