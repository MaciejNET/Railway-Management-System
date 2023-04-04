using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Admin;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IAdminService
{
    Task<ErrorOr<AdminDto>> CreateAdmin(CreateAdmin createAdmin);
    Task<ErrorOr<string>> LoginAdmin(LoginAdmin loginAdmin);
}