using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Station;

internal sealed class DeleteStationHandler(
    IStationRepository stationRepository,
    IStationScheduleRepository stationScheduleRepository)
    : ICommandHandler<DeleteStation>
{
    public async Task HandleAsync(DeleteStation command)
    {
        var stationId = new StationId(command.Id);

        var station = await stationRepository.GetByIdAsync(stationId);

        if (station is null)
        {
            throw new StationNotFoundException(stationId);
        }

        var isAnyScheduleInStation = await stationScheduleRepository.IsAnyScheduleInStation(station);

        if (isAnyScheduleInStation)
        {
            throw new StationScheduleExistsException();
        }

        await stationRepository.DeleteAsync(station);
    }
}