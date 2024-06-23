using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Passenger;

internal sealed class RemovePassengerHandler(IPassengerRepository passengerRepository)
    : ICommandHandler<RemovePassenger>
{
    public async Task HandleAsync(RemovePassenger command)
    {
        var passenger = await passengerRepository.GetByIdAsync(command.Id);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(command.Id);
        }

        await passengerRepository.DeleteAsync(passenger);
    }
}