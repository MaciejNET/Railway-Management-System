using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.RailwayEmployee;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Services.Implementations;

public class RailwayEmployeeService : IRailwayEmployeeService
{
    private readonly IRailwayEmployeeRepository _railwayEmployeeRepository;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public RailwayEmployeeService(IRailwayEmployeeRepository railwayEmployeeRepository, IAuthService authService, IMapper mapper)
    {
        _railwayEmployeeRepository = railwayEmployeeRepository;
        _authService = authService;
        _mapper = mapper;
    }

    public async Task<RailwayEmployeeDto> CreateRailwayEmployee(CreateRailwayEmployee createRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeRepository.GetByNameAsync(createRailwayEmployee.Name);

        if (railwayEmployee is not null)
        {
            throw new InvalidCredentialsException();
        }

        var name = new RailwayEmployeeName(createRailwayEmployee.Name);
        var firstName = new FirstName(createRailwayEmployee.FirstName);
        var lastName = new LastName(createRailwayEmployee.LastName);

        _authService.CreatePasswordHash(createRailwayEmployee.Password, out var passwordHash, out var passwordSalt);

        railwayEmployee = RailwayEmployee.Create(
            name,
            firstName,
            lastName,
            passwordHash,
            passwordSalt);

        await _railwayEmployeeRepository.AddAsync(railwayEmployee);

        return _mapper.Map<RailwayEmployeeDto>(railwayEmployee);
    }

    public async Task<string> LoginRailwayEmployee(LoginRailwayEmployee loginRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeRepository.GetByNameAsync(loginRailwayEmployee.Name);
        
        if (railwayEmployee is null || !_authService.VerifyPasswordHash(loginRailwayEmployee.Password, railwayEmployee.PasswordHash, railwayEmployee.PasswordSalt))
        {
            throw new InvalidCredentialsException();
        }
        
        return _authService.CreateToken(railwayEmployee);
    }
}