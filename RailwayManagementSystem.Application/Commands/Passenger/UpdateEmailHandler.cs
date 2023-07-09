using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Passenger;

internal sealed class UpdateEmailHandler : ICommandHandler<UpdateEmail>
{
    private readonly IPassengerRepository _passengerRepository;

    public UpdateEmailHandler(IPassengerRepository passengerRepository)
    {
        _passengerRepository = passengerRepository;
    }

    public async Task HandleAsync(UpdateEmail command)
    {
        var email = new Email(command.Email);
        
        var passenger = await _passengerRepository.GetByIdAsync(command.Id);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(command.Id);
        }

        var isEmailAlreadyUsed = await _passengerRepository.ExistsByEmailAsync(command.Email);

        if (isEmailAlreadyUsed)
        {
            throw new PassengerWithGivenEmailAlreadyExistsException(command.Email);
        }
        
        passenger.UpdateEmail(email);
        await _passengerRepository.UpdateAsync(passenger);
    }
}