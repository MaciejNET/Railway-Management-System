using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Passenger;

internal sealed class LoginPassengerHandler(
    IPassengerRepository passengerRepository,
    IAuthenticator authenticator,
    IPasswordManager passwordManager,
    ITokenStorage tokenStorage)
    : ICommandHandler<LoginPassenger>
{
    public async Task HandleAsync(LoginPassenger command)
    {
        var email = new Email(command.Email);

        var passenger = await passengerRepository.GetByEmailAsync(email);

        if (passenger is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!passwordManager.Validate(command.Password, passenger.Password))
        {
            throw new InvalidCredentialsException();
        }

        var jwt = authenticator.CreateToken(passenger.Id, passenger.Role.ToString().ToLowerInvariant());
        tokenStorage.Set(jwt);
    }
}