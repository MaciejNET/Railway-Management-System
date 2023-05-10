using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.DTOs;
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

    public async Task<ErrorOr<PassengerDto>> GetById(int id)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);
        
        if (passenger is null)
        {
            return Error.NotFound($"User with id: '{id}' does not exist");
        }

        return _mapper.Map<PassengerDto>(passenger);
    }

    public async Task<ErrorOr<IEnumerable<PassengerDto>>> GetAll()
    {
        var passengers = await _passengerRepository.GetAllAsync();
        
        if (!passengers.Any())
        {
            return Error.NotFound("Cannot find any passengers");
        }

        var passengersDto = _mapper.Map<IEnumerable<PassengerDto>>(passengers);
        
        return passengersDto.ToList();
    }

    public async Task<ErrorOr<string>> Login(LoginPassenger loginPassenger)
    {
        var passenger = await _passengerRepository.GetByEmailAsync(loginPassenger.Email);
        
        if (passenger is null
            || !_authService.VerifyPasswordHash(loginPassenger.Password, passenger.PasswordHash, passenger.PasswordSalt))
        {
            return Error.Validation("Invalid credentials");
        }

        return _authService.CreateToken(passenger);
    }

    public async Task<ErrorOr<PassengerDto>> Register(RegisterPassenger registerPassenger)
    {
        if (await _passengerRepository.GetByEmailAsync(registerPassenger.Email) is not null)
        {
            return Error.Validation($"Passenger with email: '{registerPassenger.Email}' already exists.");
        }

        if (await _passengerRepository.GetByPhoneNumberAsync(registerPassenger.PhoneNumber) is not null)
        {
            return Error.Validation($"Passenger with phone number: '{registerPassenger.PhoneNumber}' already exists.");
        }

        var discount = await _discountRepository.GetByNameAsync(registerPassenger.DiscountName);

        if (discount is null && !string.IsNullOrWhiteSpace(registerPassenger.DiscountName))
        {
            return Error.NotFound($"Discount with name: {registerPassenger.DiscountName} does not exists.");
        }

        _authService.CreatePasswordHash(registerPassenger.Password, out var passwordHash, out var passwordSalt);

        ErrorOr<FirstName> firstName = FirstName.Create(registerPassenger.FirstName);
        ErrorOr<LastName> lastName = LastName.Create(registerPassenger.LastName);
        ErrorOr<Email> email = Email.Create(registerPassenger.Email);
        ErrorOr<PhoneNumber> phoneNumber = PhoneNumber.Create(registerPassenger.PhoneNumber);

        if (firstName.IsError || lastName.IsError || email.IsError || phoneNumber.IsError)
        {
            List<Error> errors = new();
            
            if (firstName.IsError) errors.AddRange(firstName.Errors);
            if (lastName.IsError) errors.AddRange(lastName.Errors);
            if (email.IsError) errors.AddRange(email.Errors);
            if (phoneNumber.IsError) errors.AddRange(phoneNumber.Errors);

            return errors;
        }

        var passenger = Passenger.Create(
            firstName.Value,
            lastName.Value,
            email.Value,
            phoneNumber.Value,
            registerPassenger.Age,
            discount,
            passwordHash,
            passwordSalt);

        await _passengerRepository.AddAsync(passenger);

        return _mapper.Map<PassengerDto>(passenger);
    }

    public async Task<ErrorOr<Updated>> Update(int id, UpdatePassenger updatePassenger)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            return Error.NotFound($"User with id: '{id}' does not exist");
        }

        if (updatePassenger.Email is not null)
        {
            if (await _passengerRepository.GetByEmailAsync(updatePassenger.Email) is not null)
            {
                return Error.Validation($"User with email: '{updatePassenger.Email}' already exists.");
            }

            ErrorOr<Email> email = Email.Create(updatePassenger.Email);

            if (email.IsError)
            {
                return email.Errors;
            }

            passenger.Email = email.Value;
        }

        if (updatePassenger.PhoneNumber is not null)
        {
            if (await _passengerRepository.GetByPhoneNumberAsync(updatePassenger.PhoneNumber) is not null)
            {
                return Error.Validation($"User with phone number: '{updatePassenger.PhoneNumber}' already exists.");
            }

            ErrorOr<PhoneNumber> phoneNumber = PhoneNumber.Create(updatePassenger.PhoneNumber);

            if (phoneNumber.IsError)
            {
                return phoneNumber.Errors;
            }

            passenger.PhoneNumber = phoneNumber.Value;
        }

        if (updatePassenger.Password is not null)
        {
            if (_authService.VerifyPasswordHash(updatePassenger.Password, passenger.PasswordHash,
                    passenger.PasswordSalt))
            {
                return Error.Validation("New password must be different");
            }
            
            _authService.CreatePasswordHash(updatePassenger.Password, out var passwordHash, out var passwordSalt);
            passenger.PasswordHash = passwordHash;
            passenger.PasswordSalt = passwordSalt;
        }
        
        await _passengerRepository.UpdateAsync(passenger);

        return Result.Updated;
    }

    public async Task<ErrorOr<Updated>> UpdateDiscount(int id, string? discountName)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            return Error.NotFound($"User with id: '{id}' does not exist");
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
                return Error.NotFound($"Discount with name: '{discountName}' does not exist");
            }
        }

        await _passengerRepository.UpdateAsync(passenger);

        return Result.Updated;
    }

    public async Task<ErrorOr<Deleted>> Delete(int id)
    {
        var passenger = await _passengerRepository.GetByIdAsync(id);

        if (passenger is null)
        {
            return Error.NotFound($"User with id: '{id}' does not exist");
        }

        await _passengerRepository.RemoveAsync(passenger);

        return Result.Deleted;
    }
}