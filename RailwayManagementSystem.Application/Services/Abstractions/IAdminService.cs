using ErrorOr;
using RailwayManagementSystem.Application.Commands.Admin;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IAdminService
{
    Task<ErrorOr<AdminDto>> CreateAdmin(CreateAdmin createAdmin);
    Task<ErrorOr<string>> LoginAdmin(LoginAdmin loginAdmin);
}