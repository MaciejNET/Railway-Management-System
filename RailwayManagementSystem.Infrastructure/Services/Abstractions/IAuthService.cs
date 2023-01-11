using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IAuthService
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    public string CreateToken(Passenger passenger);
    public string CreateToken(Admin admin);
    public string CreateToken(RailwayEmployee railwayEmployee);
    
}