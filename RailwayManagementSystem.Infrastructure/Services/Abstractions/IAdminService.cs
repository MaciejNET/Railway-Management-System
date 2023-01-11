using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Admin;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IAdminService
{
    Task<ServiceResponse<AdminDto>> CreateAdmin(CreateAdmin createAdmin);
    Task<ServiceResponse<string>> LoginAdmin(LoginAdmin loginAdmin);
}