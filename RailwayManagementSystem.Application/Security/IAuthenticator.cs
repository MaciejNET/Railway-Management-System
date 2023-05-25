using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}