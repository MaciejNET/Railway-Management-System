using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

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
        var adminId = new UserId(command.Id);
        var name = new AdminName(command.Name);
        var password = new Password(command.Password);
        
        var adminAlreadyExists = await _adminRepository.ExistByNameAsync(name);

        if (adminAlreadyExists)
        {
            throw new AdminAlreadyExistsException(name);
        }

        var securedPassword = _passwordManager.Secure(password);

        var admin = Core.Entities.Admin.Create(adminId, name, securedPassword);

        await _adminRepository.AddAsync(admin);
    }
}