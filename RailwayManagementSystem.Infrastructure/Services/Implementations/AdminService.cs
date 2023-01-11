using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.Commands.Admin;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

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


        try
        {
            admin = new Admin
            {
                Name = createAdmin.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Role.Admin
            };

            await _adminRepository.Add(admin);
            await _adminRepository.SaveChangesAsync();
        
            var response = new ServiceResponse<AdminDto>
            {
                Data = _mapper.Map<AdminDto>(admin)
            };

            return response;
        }
        catch (Exception e)
        {
            var response = new ServiceResponse<AdminDto>
            {
                Success = false,
                Message = e.Message
            };

            return response;
        }
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

        if (!_authService.VerifyPasswordHash(loginAdmin.Password, admin.PasswordHash, admin.PasswordSalt))
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