using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Admin;

public class LoginAdminHandler : ICommandHandler<LoginAdmin>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IPasswordManager _passwordManager;
    private readonly IAuthenticator _authenticator;
    private readonly ITokenStorage _tokenStorage;

    public LoginAdminHandler(IAdminRepository adminRepository, IPasswordManager passwordManager, IAuthenticator authenticator, ITokenStorage tokenStorage)
    {
        _adminRepository = adminRepository;
        _passwordManager = passwordManager;
        _authenticator = authenticator;
        _tokenStorage = tokenStorage;
    }

    public async Task HandleAsync(LoginAdmin command)
    {
        var admin = await _adminRepository.GetByNameAsync(command.Name);

        if (admin is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.Validate(command.Password, admin.Password))
        {
            throw new InvalidCredentialsException();
        }

        var jwt = _authenticator.CreateToken(admin.Id, admin.Role.ToString().ToLowerInvariant());
        _tokenStorage.Set(jwt);
    }
}