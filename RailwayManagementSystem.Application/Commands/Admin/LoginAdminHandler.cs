using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Admin;

internal sealed class LoginAdminHandler(
    IAdminRepository adminRepository,
    IPasswordManager passwordManager,
    IAuthenticator authenticator,
    ITokenStorage tokenStorage)
    : ICommandHandler<LoginAdmin>
{
    public async Task HandleAsync(LoginAdmin command)
    {
        var name = new AdminName(command.Name);
        
        var admin = await adminRepository.GetByNameAsync(name);

        if (admin is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!passwordManager.Validate(command.Password, admin.Password))
        {
            throw new InvalidCredentialsException();
        }

        var jwt = authenticator.CreateToken(admin.Id, admin.Role.ToString().ToLowerInvariant());
        tokenStorage.Set(jwt);
    }
}