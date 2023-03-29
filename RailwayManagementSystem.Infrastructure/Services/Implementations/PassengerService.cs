using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.Commands.Passenger;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

public class PassengerService : IPassengerService
{
    private readonly IAuthService _authService;
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;
    private readonly IPassengerRepository _passengerRepository;

    public PassengerService(IPassengerRepository passengerRepository, IDiscountRepository discountRepository,
        IMapper mapper, IAuthService authService)
    {
        _passengerRepository = passengerRepository;
        _discountRepository = discountRepository;
        _mapper = mapper;
        _authService = authService;
    }

    public async Task<ServiceResponse<PassengerDto>> GetById(int id)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = $"User with id: '{id}' does not exist"
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<PassengerDto>
        {
            Data = _mapper.Map<PassengerDto>(passenger)
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<PassengerDto>>> GetAll()
    {
        var passengers = await _passengerRepository.GetAllAsync();
        if (!passengers.Any())
        {
            var serviceResponse = new ServiceResponse<IEnumerable<PassengerDto>>
            {
                Success = false,
                Message = "Cannot find any passengers"
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<IEnumerable<PassengerDto>>
        {
            Data = _mapper.Map<IEnumerable<PassengerDto>>(passengers)
        };
        return response;
    }

    public async Task<ServiceResponse<string>> Login(LoginPassenger loginPassenger)
    {
        var passenger = await _passengerRepository.GetByEmailAsync(loginPassenger.Email);
        if (passenger is null)
        {
            var serviceResponse = new ServiceResponse<string>
            {
                Success = false,
                Message = $"Passenger with email: '{loginPassenger.Email}' does not exists."
            };

            return serviceResponse;
        }

        if (_authService.VerifyPasswordHash(loginPassenger.Password, passenger.PasswordHash, passenger.PasswordSalt) is
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
            Data = _authService.CreateToken(passenger)
        };

        return response;
    }

    public async Task<ServiceResponse<PassengerDto>> Register(RegisterPassenger registerPassenger)
    {
        if (await _passengerRepository.GetByEmailAsync(registerPassenger.Email) is not null)
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = $"Passenger with email: '{registerPassenger.Email}' already exists."
            };

            return serviceResponse;
        }

        if (await _passengerRepository.GetByPhoneNumberAsync(registerPassenger.PhoneNumber) is not null)
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = $"Passenger with phone number: '{registerPassenger.PhoneNumber}' already exists."
            };

            return serviceResponse;
        }

        var discount = await _discountRepository.GetByNameAsync(registerPassenger.DiscountName);

        if (discount is null && !string.IsNullOrWhiteSpace(registerPassenger.DiscountName))
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = "Discount does not exists."
            };

            return serviceResponse;
        }

        _authService.CreatePasswordHash(registerPassenger.Password, out var passwordHash, out var passwordSalt);
        try
        {
            var passenger = new Passenger
            {
                FirstName = registerPassenger.FirstName,
                LastName = registerPassenger.LastName,
                Email = registerPassenger.Email,
                PhoneNumber = registerPassenger.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Age = registerPassenger.Age,
                Discount = discount,
                Role = Role.Passenger
            };

            await _passengerRepository.AddAsync(passenger);
            await _passengerRepository.SaveChangesAsync();
        
            var response = new ServiceResponse<PassengerDto>
            {
                Data = _mapper.Map<PassengerDto>(passenger)
            };

            return response;
        }
        catch (Exception e)
        {
            var response = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = e.Message
            };

            return response;
        }
    }

    public async Task<ServiceResponse<PassengerDto>> Update(int id, UpdatePassenger updatePassenger)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = $"User with id: '{id}' does not exist"
            };

            return serviceResponse;
        }

        if (updatePassenger.Email is not null && await _passengerRepository.GetByEmailAsync(updatePassenger.Email) is null)
        {
            passenger.Email = updatePassenger.Email;
        }
        else if (updatePassenger.Email is not null)
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = $"User with email: '{updatePassenger.Email}' already exists."
            };

            return serviceResponse;
        }

        if (updatePassenger.PhoneNumber is not null &&
            await _passengerRepository.GetByPhoneNumberAsync(updatePassenger.Password) is null)
        {
            passenger.PhoneNumber = updatePassenger.PhoneNumber;
        }
        else if (updatePassenger.PhoneNumber is not null)
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = $"User with phone number: '{updatePassenger.PhoneNumber}' already exists."
            };

            return serviceResponse;
        }

        if (updatePassenger.Password is not null &&
            _authService.VerifyPasswordHash(updatePassenger.Password, passenger.PasswordHash, passenger.PasswordSalt) is
                false)
        {
            _authService.CreatePasswordHash(updatePassenger.Password, out var passwordHash, out var passwordSalt);
            passenger.PasswordHash = passwordHash;
            passenger.PasswordSalt = passwordSalt;
        }
        else if (updatePassenger.Password is not null)
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = "Same password."
            };

            return serviceResponse;
        }

        await _passengerRepository.UpdateAsync(passenger);
        await _passengerRepository.SaveChangesAsync();

        var response = new ServiceResponse<PassengerDto>
        {
            Data = _mapper.Map<PassengerDto>(passenger)
        };

        return response;
    }

    public async Task<ServiceResponse<PassengerDto>> UpdateDiscount(int id, string? discountName)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = $"User with id: '{id}' does not exist"
            };

            return serviceResponse;
        }

        if (discountName is null)
        {
            passenger.Discount = null;
        }
        else
        {
            var discount = await _discountRepository.GetByNameAsync(discountName);

            if (discount is null)
            {
                var serviceResponse = new ServiceResponse<PassengerDto>
                {
                    Success = false,
                    Message = $"Discount with name: '{discountName}' does not exist"
                };

                return serviceResponse;
            }
        }

        await _passengerRepository.UpdateAsync(passenger);

        var response = new ServiceResponse<PassengerDto>
        {
            Data = _mapper.Map<PassengerDto>(passenger)
        };

        return response;
    }

    public async Task<ServiceResponse<PassengerDto>> Delete(int id)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            var serviceResponse = new ServiceResponse<PassengerDto>
            {
                Success = false,
                Message = $"User with id: '{id}' does not exist"
            };

            return serviceResponse;
        }

        await _passengerRepository.RemoveAsync(passenger);

        var response = new ServiceResponse<PassengerDto>
        {
            Data = _mapper.Map<PassengerDto>(passenger)
        };

        return response;
    }
}