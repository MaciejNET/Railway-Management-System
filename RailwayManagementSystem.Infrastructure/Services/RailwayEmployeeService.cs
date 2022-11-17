using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.Commands.RailwayEmployee;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

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

    public async Task<ServiceResponse<RailwayEmployeeDto>> CreateRailwayEmployee(CreateRailwayEmployee createRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeRepository.GetByName(createRailwayEmployee.Name);

        if (railwayEmployee is not null)
        {
            var serviceResponse = new ServiceResponse<RailwayEmployeeDto>()
            {
                Success = false,
                Message = $"Railway employee with name: {createRailwayEmployee.Name} already exists."
            };

            return serviceResponse;
        }
        
        _authService.CreatePasswordHash(createRailwayEmployee.Password, out var passwordHash, out var passwordSalt);

        railwayEmployee = new RailwayEmployee
        {
            Name = createRailwayEmployee.Name,
            FirstName = createRailwayEmployee.FirstName,
            LastName = createRailwayEmployee.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = Role.RailwayEmployee
        };

        await _railwayEmployeeRepository.Add(railwayEmployee);
        
        var response = new ServiceResponse<RailwayEmployeeDto>
        {
            Data = _mapper.Map<RailwayEmployeeDto>(railwayEmployee)
        };

        return response;
    }

    public async Task<ServiceResponse<string>> LoginRailwayEmployee(LoginRailwayEmployee loginRailwayEmployee)
    {
        var railwayEmployee = await _railwayEmployeeRepository.GetByName(loginRailwayEmployee.Name);
        if (railwayEmployee is null)
        {
            var serviceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = $"Railway employee with name: '{loginRailwayEmployee.Name}' does not exists."
            };

            return serviceResponse;
        }

        if (_authService.VerifyPasswordHash(loginRailwayEmployee.Password, railwayEmployee.PasswordHash, railwayEmployee.PasswordSalt) is
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
            Data = _authService.CreateToken(railwayEmployee)
        };

        return response;
    }
}