using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Admin;

internal sealed class DeleteAdminHandler(IAdminRepository adminRepository) : ICommandHandler<DeleteAdmin>
{
    public async Task HandleAsync(DeleteAdmin command)
    {
        var adminId = new UserId(command.AdminId);

        var admin = await adminRepository.GetByIdAsync(adminId);

        if (admin is null)
        {
            throw new AdminNotFoundException(adminId);
        }

        await adminRepository.DeleteAsync(admin);
    }
}