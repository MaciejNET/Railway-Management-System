using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

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
        var passengerId = new UserId(command.Id);
        var firstName = new FirstName(command.FirstName);
        var lastName = new LastName(command.LastName);
        var email = new Email(command.Email);
        var phoneNumber = new PhoneNumber(command.PhoneNumber);
        var password = new Password(command.Password);
        
        var passengerAlreadyExists = await _passengerRepository.ExistsByEmailAsync(email);

        if (passengerAlreadyExists)
        {
            throw new PassengerWithGivenEmailAlreadyExists(command.Email);
        }

        var securedPassword = _passwordManager.Secure(password);

        Core.Entities.Passenger passenger;
        if (!string.IsNullOrWhiteSpace(command.DiscountName))
        {
            var discount = await _discountRepository.GetByNameAsync(command.DiscountName);

            if (discount is null)
            {
                throw new DiscountNotFoundException(command.DiscountName);
            }

            passenger = Core.Entities.Passenger.CreateWithDiscount(
                passengerId,
                firstName,
                lastName,
                email,
                phoneNumber,
                command.Age,
                discount,
                securedPassword
            );
        }
        else
        {
            passenger = Core.Entities.Passenger.Create(
                passengerId,
                firstName,
                lastName,
                email,
                phoneNumber,
                command.Age,
                securedPassword
            );
        }

        await _passengerRepository.AddAsync(passenger);
    }
}