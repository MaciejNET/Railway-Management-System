using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Passenger;

internal sealed class UpdateEmailHandler(IPassengerRepository passengerRepository) : ICommandHandler<UpdateEmail>
{
    public async Task HandleAsync(UpdateEmail command)
    {
        var email = new Email(command.Email);
        
        var passenger = await passengerRepository.GetByIdAsync(command.Id);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(command.Id);
        }

        var isEmailAlreadyUsed = await passengerRepository.ExistsByEmailAsync(command.Email);

        if (isEmailAlreadyUsed)
        {
            throw new PassengerWithGivenEmailAlreadyExistsException(command.Email);
        }
        
        passenger.UpdateEmail(email);
        await passengerRepository.UpdateAsync(passenger);
    }
}