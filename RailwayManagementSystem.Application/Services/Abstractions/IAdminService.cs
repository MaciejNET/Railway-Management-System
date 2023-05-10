using ErrorOr;
using RailwayManagementSystem.Application.Commands.Admin;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IAdminService
{
    Task<AdminDto> CreateAdmin(CreateAdmin createAdmin);
    Task<string> LoginAdmin(LoginAdmin loginAdmin);
}