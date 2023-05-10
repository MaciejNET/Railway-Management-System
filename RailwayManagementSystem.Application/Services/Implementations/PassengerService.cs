using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Services.Implementations;

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

    public async Task<PassengerDto> GetById(int id)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);
        
        if (passenger is null)
        {
            throw new PassengerNotFoundException(id);
        }

        return _mapper.Map<PassengerDto>(passenger);
    }

    public async Task<IEnumerable<PassengerDto>> GetAll()
    {
        var passengers = await _passengerRepository.GetAllAsync();
        
        if (!passengers.Any())
        {
            return new List<PassengerDto>();
        }

        var passengersDto = _mapper.Map<IEnumerable<PassengerDto>>(passengers);
        
        return passengersDto.ToList();
    }

    public async Task<string> Login(LoginPassenger loginPassenger)
    {
        var passenger = await _passengerRepository.GetByEmailAsync(loginPassenger.Email);
        
        if (passenger is null
            || !_authService.VerifyPasswordHash(loginPassenger.Password, passenger.PasswordHash, passenger.PasswordSalt))
        {
            throw new InvalidCredentialsException();
        }

        return _authService.CreateToken(passenger);
    }

    public async Task<PassengerDto> Register(RegisterPassenger registerPassenger)
    {
        if (await _passengerRepository.GetByEmailAsync(registerPassenger.Email) is not null)
        {
            throw new PassengerWithGivenEmailAlreadyExists(registerPassenger.Email);
        }

        if (await _passengerRepository.GetByPhoneNumberAsync(registerPassenger.PhoneNumber) is not null)
        {
            throw new PassengerWithGivenPhoneNumberAlreadyExists(registerPassenger.PhoneNumber);
        }

        var discount = await _discountRepository.GetByNameAsync(registerPassenger.DiscountName);

        if (discount is null && !string.IsNullOrWhiteSpace(registerPassenger.DiscountName))
        {
            throw new DiscountNotFoundException(registerPassenger.DiscountName);
        }

        _authService.CreatePasswordHash(registerPassenger.Password, out var passwordHash, out var passwordSalt);

        var firstName = new FirstName(registerPassenger.FirstName);
        var lastName = new LastName(registerPassenger.LastName);
        var email = new Email(registerPassenger.Email);
        var phoneNumber = new PhoneNumber(registerPassenger.PhoneNumber);
        

        var passenger = Passenger.Create(
            firstName,
            lastName,
            email,
            phoneNumber,
            registerPassenger.Age,
            discount,
            passwordHash,
            passwordSalt);

        await _passengerRepository.AddAsync(passenger);

        return _mapper.Map<PassengerDto>(passenger);
    }

    public async Task<Updated> Update(int id, UpdatePassenger updatePassenger)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(id);
        }

        if (updatePassenger.Email is not null)
        {
            if (await _passengerRepository.GetByEmailAsync(updatePassenger.Email) is not null)
            {
                throw new PassengerWithGivenEmailAlreadyExists(updatePassenger.Email);
            }

            var email = new Email(updatePassenger.Email);

            passenger.Email = email.Value;
        }

        if (updatePassenger.PhoneNumber is not null)
        {
            if (await _passengerRepository.GetByPhoneNumberAsync(updatePassenger.PhoneNumber) is not null)
            {
                throw new PassengerWithGivenPhoneNumberAlreadyExists(updatePassenger.PhoneNumber);
            }

            var phoneNumber = new PhoneNumber(updatePassenger.PhoneNumber);

            passenger.PhoneNumber = phoneNumber.Value;
        }

        if (updatePassenger.Password is not null)
        {
            if (_authService.VerifyPasswordHash(updatePassenger.Password, passenger.PasswordHash,
                    passenger.PasswordSalt))
            {
                throw new InvalidCredentialsException();
            }
            
            _authService.CreatePasswordHash(updatePassenger.Password, out var passwordHash, out var passwordSalt);
            passenger.PasswordHash = passwordHash;
            passenger.PasswordSalt = passwordSalt;
        }
        
        await _passengerRepository.UpdateAsync(passenger);

        return Result.Updated;
    }

    public async Task UpdateDiscount(int id, string? discountName)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(id);
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
                throw new DiscountNotFoundException(discountName);
            }
        }

        await _passengerRepository.UpdateAsync(passenger);
    }

    public async Task Delete(int id)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(id);
        }

        await _passengerRepository.RemoveAsync(passenger);
    }
}