using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Admin;

internal sealed class DeleteAdminHandler : ICommandHandler<DeleteAdmin>
{
    private readonly IAdminRepository _adminRepository;

    public DeleteAdminHandler(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public async Task HandleAsync(DeleteAdmin command)
    {
        var adminId = new UserId(command.AdminId);

        var admin = await _adminRepository.GetByIdAsync(adminId);

        if (admin is null)
        {
            throw new AdminNotFoundException(adminId);
        }

        await _adminRepository.DeleteAsync(admin);
    }
}