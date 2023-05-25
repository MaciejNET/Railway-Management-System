using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Passenger;

public class RegisterPassengerHandler : ICommandHandler<RegisterPassenger>
{
    private readonly IPassengerRepository _passengerRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IPasswordManager _passwordManager;

    public RegisterPassengerHandler(IPassengerRepository passengerRepository, IDiscountRepository discountRepository, IPasswordManager passwordManager)
    {
        _passengerRepository = passengerRepository;
        _discountRepository = discountRepository;
        _passwordManager = passwordManager;
    }

    public async Task HandleAsync(RegisterPassenger command)
    {
        var passengerAlreadyExists = await _passengerRepository.ExistsByEmailAsync(command.Email);

        if (passengerAlreadyExists)
        {
            throw new PassengerWithGivenEmailAlreadyExists(command.Email);
        }

        var securedPassword = _passwordManager.Secure(command.Password);

        Core.Entities.Passenger passenger;
        if (!string.IsNullOrWhiteSpace(command.DiscountName))
        {
            var discount = await _discountRepository.GetByNameAsync(command.DiscountName);

            if (discount is null)
            {
                throw new DiscountNotFoundException(command.DiscountName);
            }

            passenger = Core.Entities.Passenger.CreateWithDiscount(
                command.Id,
                command.FirstName,
                command.LastName,
                command.Email,
                command.PhoneNumber,
                command.Age,
                discount,
                securedPassword
            );
        }
        else
        {
            passenger = Core.Entities.Passenger.Create(
                command.Id,
                command.FirstName,
                command.LastName,
                command.Email,
                command.PhoneNumber,
                command.Age,
                securedPassword
            );
        }

        await _passengerRepository.AddAsync(passenger);
    }
}