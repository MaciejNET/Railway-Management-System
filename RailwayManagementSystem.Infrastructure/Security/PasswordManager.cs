using Microsoft.AspNetCore.Identity;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Infrastructure.Security;

internal sealed class PasswordManager(IPasswordHasher<User> passwordHasher) : IPasswordManager
{
    public string Secure(string password)
        => passwordHasher.HashPassword(default, password);

    public bool Validate(string password, string securedPassword)
        => passwordHasher.VerifyHashedPassword(default, securedPassword, password) ==
           PasswordVerificationResult.Success;
}