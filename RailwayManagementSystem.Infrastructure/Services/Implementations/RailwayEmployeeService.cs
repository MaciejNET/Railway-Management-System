using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;
using RailwayManagementSystem.Infrastructure.Commands.RailwayEmployee;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

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

    public async Task<ErrorOr<RailwayEmployeeDto>> CreateRailwayEmployee(CreateRailwayEmployee createRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeRepository.GetByNameAsync(createRailwayEmployee.Name);

        if (railwayEmployee is not null)
        {
            return Error.Validation($"Railway employee with name: {createRailwayEmployee.Name} already exists.");
        }

        ErrorOr<RailwayEmployeeName> name = RailwayEmployeeName.Create(createRailwayEmployee.Name);
        ErrorOr<FirstName> firstName = FirstName.Create(createRailwayEmployee.FirstName);
        ErrorOr<LastName> lastName = LastName.Create(createRailwayEmployee.LastName);

        if (name.IsError || firstName.IsError || lastName.IsError)
        {
            List<Error> errors = new();
            
            if (name.IsError) errors.AddRange(name.Errors);
            if (firstName.IsError) errors.AddRange(firstName.Errors);
            if (lastName.IsError) errors.AddRange(lastName.Errors);

            return errors;
        }

        _authService.CreatePasswordHash(createRailwayEmployee.Password, out var passwordHash, out var passwordSalt);

        railwayEmployee = RailwayEmployee.Create(
            name.Value,
            firstName.Value,
            lastName.Value,
            passwordHash,
            passwordSalt);

        await _railwayEmployeeRepository.AddAsync(railwayEmployee);

        return _mapper.Map<RailwayEmployeeDto>(railwayEmployee);
    }

    public async Task<ErrorOr<string>> LoginRailwayEmployee(LoginRailwayEmployee loginRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeRepository.GetByNameAsync(loginRailwayEmployee.Name);
        
        if (railwayEmployee is null || !_authService.VerifyPasswordHash(loginRailwayEmployee.Password, railwayEmployee.PasswordHash, railwayEmployee.PasswordSalt))
        {
            return Error.Validation("Invalid credentials.");
        }
        
        return _authService.CreateToken(railwayEmployee);
    }
}