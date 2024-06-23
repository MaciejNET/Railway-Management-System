using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Passenger;

internal sealed class RegisterPassengerHandler(
    IPassengerRepository passengerRepository,
    IDiscountRepository discountRepository,
    IPasswordManager passwordManager)
    : ICommandHandler<RegisterPassenger>
{
    public async Task HandleAsync(RegisterPassenger command)
    {
        var passengerId = new UserId(command.Id);
        var firstName = new FirstName(command.FirstName);
        var lastName = new LastName(command.LastName);
        var email = new Email(command.Email);
        var dateOfBirth = new DateOfBirth(command.DateOfBirth);

        var passengerAlreadyExists = await passengerRepository.ExistsByEmailAsync(email);

        if (passengerAlreadyExists)
        {
            throw new PassengerWithGivenEmailAlreadyExistsException(command.Email);
        }

        var isPasswordValid = Password.ValidatePassword(command.Password);

        if (!isPasswordValid)
        {
            throw new InvalidPasswordException();
        }

        var securedPassword = passwordManager.Secure(command.Password);

        Core.Entities.Passenger passenger;
        if (!string.IsNullOrWhiteSpace(command.DiscountName))
        {
            var discount = await discountRepository.GetByNameAsync(command.DiscountName);

            if (discount is null)
            {
                throw new DiscountNotFoundException(command.DiscountName);
            }

            passenger = Core.Entities.Passenger.CreateWithDiscount(
                passengerId,
                firstName,
                lastName,
                email,
                dateOfBirth,
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
                dateOfBirth,
                securedPassword
            );
        }

        await passengerRepository.AddAsync(passenger);
    }
}