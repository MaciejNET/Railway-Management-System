using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Trip;

internal sealed class DeleteTripHandler : ICommandHandler<DeleteTrip>
{
    private readonly ITripRepository _tripRepository;

    public DeleteTripHandler(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task HandleAsync(DeleteTrip command)
    {
        var tripId = new TripId(command.Id);

        var trip = await _tripRepository.GetByIdAsync(tripId);

        if (trip is null)
        {
            throw new TripNotFoundException(tripId);
        }

        await _tripRepository.DeleteAsync(trip);
    }
}