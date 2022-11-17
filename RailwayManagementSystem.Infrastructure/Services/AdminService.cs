using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.Commands.Admin;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

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

    public async Task<ServiceResponse<AdminDto>> CreateAdmin(CreateAdmin createAdmin)
    {
        var admin = await _adminRepository.GetByName(createAdmin.Name);

        if (admin is not null)
        {
            var serviceResponse = new ServiceResponse<AdminDto>()
            {
                Success = false,
                Message = $"Admin with name: {createAdmin.Name} already exists."
            };

            return serviceResponse;
        }
        
        _authService.CreatePasswordHash(createAdmin.Password, out var passwordHash, out var passwordSalt);

        admin = new Admin
        {
            Name = createAdmin.Name,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = Role.Admin
        };

        await _adminRepository.Add(admin);
        
        var response = new ServiceResponse<AdminDto>
        {
            Data = _mapper.Map<AdminDto>(admin)
        };

        return response;
    }

    public async Task<ServiceResponse<string>> LoginAdmin(LoginAdmin loginAdmin)
    {
        var admin = await _adminRepository.GetByName(loginAdmin.Name);
        if (admin is null)
        {
            var serviceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = $"Admin with name: '{loginAdmin.Name}' does not exists."
            };

            return serviceResponse;
        }

        if (_authService.VerifyPasswordHash(loginAdmin.Password, admin.PasswordHash, admin.PasswordSalt) is
            false)
        {
            var serviceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = "Invalid password."
            };

            return serviceResponse;
        }
        
        var response = new ServiceResponse<string>
        {
            Data = _authService.CreateToken(admin)
        };

        return response;
    }
}