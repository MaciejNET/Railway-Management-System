using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.Admin;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Services.Implementations;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AdminService(IAuthService authService, IAdminRepository adminRepository, IMapper mapper)
    {
        _authService = authService;
        _adminRepository = adminRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<AdminDto>> CreateAdmin(CreateAdmin createAdmin)
    {
        var admin = await _adminRepository.GetByNameAsync(createAdmin.Name);

        if (admin is not null)
        {
            return Error.Validation(description: $"Admin with name : '{createAdmin.Name}' already exists.");
        }
        
        _authService.CreatePasswordHash(createAdmin.Password, out var passwordHash, out var passwordSalt);
        
           ErrorOr<AdminName> name = AdminName.Create(createAdmin.Name);
           
           if (name.IsError)
           {
               return name.FirstError;
           }
           
           admin = Admin.Create(name.Value, passwordHash, passwordSalt);
           
           await _adminRepository.AddAsync(admin);
           
           return _mapper.Map<AdminDto>(admin);
    }

    public async Task<ErrorOr<string>> LoginAdmin(LoginAdmin loginAdmin)
    {
        var admin = await _adminRepository.GetByNameAsync(loginAdmin.Name);
        if (admin is null)
        {
            return Error.NotFound(description: $"Admin with name: '{loginAdmin.Name}' does not exists.");
        }

        if (!_authService.VerifyPasswordHash(loginAdmin.Password, admin.PasswordHash, admin.PasswordSalt))
        {
            return Error.Validation(description: "Invalid password.");
        }
        
        return _authService.CreateToken(admin);
    }
}