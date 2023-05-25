using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Admin;

public class CreateAdminHandler : ICommandHandler<CreateAdmin>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IPasswordManager _passwordManager;

    public CreateAdminHandler(IAdminRepository adminRepository, IPasswordManager passwordManager)
    {
        _adminRepository = adminRepository;
        _passwordManager = passwordManager;
    }

    public async Task HandleAsync(CreateAdmin command)
    {
        var adminAlreadyExists = await _adminRepository.ExistByNameAsync(command.Name);

        if (adminAlreadyExists)
        {
            throw new AdminAlreadyExistsException(command.Name);
        }

        var securedPassword = _passwordManager.Secure(command.Password);

        var admin = Core.Entities.Admin.Create(command.Id, command.Name, securedPassword);

        await _adminRepository.AddAsync(admin);
    }
}