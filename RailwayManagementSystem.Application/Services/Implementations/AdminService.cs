using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.Admin;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
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

    public async Task<AdminDto> CreateAdmin(CreateAdmin createAdmin)
    {
        var admin = await _adminRepository.GetByNameAsync(createAdmin.Name);

        if (admin is not null)
        {
            throw new AdminAlreadyExistsException(admin.Name);
        }
        
        _authService.CreatePasswordHash(createAdmin.Password, out var passwordHash, out var passwordSalt);
        
        var name = new AdminName(createAdmin.Name);
           
        admin = Admin.Create(name, passwordHash, passwordSalt);
        
        await _adminRepository.AddAsync(admin);
        
        return _mapper.Map<AdminDto>(admin);
    }

    public async Task<string> LoginAdmin(LoginAdmin loginAdmin)
    {
        var admin = await _adminRepository.GetByNameAsync(loginAdmin.Name);
        if (admin is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_authService.VerifyPasswordHash(loginAdmin.Password, admin.PasswordHash, admin.PasswordSalt))
        {
            throw new InvalidCredentialsException();
        }
        
        return _authService.CreateToken(admin);
    }
}