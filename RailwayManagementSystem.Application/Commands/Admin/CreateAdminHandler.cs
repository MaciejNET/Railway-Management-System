using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Admin;

internal sealed class CreateAdminHandler(IAdminRepository adminRepository, IPasswordManager passwordManager)
    : ICommandHandler<CreateAdmin>
{
    public async Task HandleAsync(CreateAdmin command)
    {
        var adminId = new UserId(command.Id);
        var name = new AdminName(command.Name);

        var isPasswordValidFormat = Password.ValidatePassword(command.Password);

        if (!isPasswordValidFormat)
        {
            throw new InvalidPasswordException();
        }
        
        var adminAlreadyExists = await adminRepository.ExistByNameAsync(name);

        if (adminAlreadyExists)
        {
            throw new AdminAlreadyExistsException(name);
        }

        var securedPassword = passwordManager.Secure(command.Password);

        var admin = Core.Entities.Admin.Create(adminId, name, securedPassword);

        await adminRepository.AddAsync(admin);
    }
}