using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Station;

internal sealed class DeleteStationHandler : ICommandHandler<DeleteStation>
{
    private readonly IStationRepository _stationRepository;
    private readonly IStationScheduleRepository _stationScheduleRepository;

    public DeleteStationHandler(IStationRepository stationRepository, IStationScheduleRepository stationScheduleRepository)
    {
        _stationRepository = stationRepository;
        _stationScheduleRepository = stationScheduleRepository;
    }

    public async Task HandleAsync(DeleteStation command)
    {
        var stationId = new StationId(command.Id);

        var station = await _stationRepository.GetByIdAsync(stationId);

        if (station is null)
        {
            throw new StationNotFoundException(stationId);
        }

        var isAnyScheduleInStation = await _stationScheduleRepository.IsAnyScheduleInStation(station);

        if (isAnyScheduleInStation)
        {
            throw new StationScheduleExistsException();
        }

        await _stationRepository.DeleteAsync(station);
    }
}