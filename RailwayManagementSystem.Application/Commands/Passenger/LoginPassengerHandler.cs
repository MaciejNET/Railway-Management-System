using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Security;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Passenger;

public class LoginPassengerHandler : ICommandHandler<LoginPassenger>
{
    private readonly IPassengerRepository _passengerRepository;
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;

    public LoginPassengerHandler(IPassengerRepository passengerRepository, IAuthenticator authenticator, IPasswordManager passwordManager, ITokenStorage tokenStorage)
    {
        _passengerRepository = passengerRepository;
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
    }

    public async Task HandleAsync(LoginPassenger command)
    {
        var passenger = await _passengerRepository.GetByEmailAsync(command.Email);

        if (passenger is null)
        {
            throw new InvalidCredentialsException();
        }

        if (!_passwordManager.Validate(command.Password, passenger.Password))
        {
            throw new InvalidCredentialsException();
        }

        var jwt = _authenticator.CreateToken(passenger.Id, passenger.Role.ToString().ToLowerInvariant());
        _tokenStorage.Set(jwt);
    }
}