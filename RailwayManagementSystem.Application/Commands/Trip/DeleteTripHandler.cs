using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Trip;

internal sealed class DeleteTripHandler(ITripRepository tripRepository) : ICommandHandler<DeleteTrip>
{
    public async Task HandleAsync(DeleteTrip command)
    {
        var tripId = new TripId(command.Id);

        var trip = await tripRepository.GetByIdAsync(tripId);

        if (trip is null)
        {
            throw new TripNotFoundException(tripId);
        }

        await tripRepository.DeleteAsync(trip);
    }
}