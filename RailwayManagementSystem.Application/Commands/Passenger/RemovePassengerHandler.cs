using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Passenger;

internal sealed class RemovePassengerHandler : ICommandHandler<RemovePassenger>
{
    private readonly IPassengerRepository _passengerRepository;

    public RemovePassengerHandler(IPassengerRepository passengerRepository)
    {
        _passengerRepository = passengerRepository;
    }

    public async Task HandleAsync(RemovePassenger command)
    {
        var passenger = await _passengerRepository.GetByIdAsync(command.Id);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(command.Id);
        }

        await _passengerRepository.DeleteAsync(passenger);
    }
}