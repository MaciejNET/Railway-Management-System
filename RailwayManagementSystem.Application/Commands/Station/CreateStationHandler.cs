using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Station;

public class CreateStationHandler : ICommandHandler<CreateStation>
{
    private readonly IStationRepository _stationRepository;

    public CreateStationHandler(IStationRepository stationRepository)
    {
        _stationRepository = stationRepository;
    }

    public async Task HandleAsync(CreateStation command)
    {
        var stationAlreadyExists = await _stationRepository.ExistsByNameAsync(command.Name);

        if (stationAlreadyExists)
        {
            throw new StationWithGivenNameAlreadyExistsException(command.Name);
        }

        var station = Core.Entities.Station.Create(command.Id, command.Name, command.City, command.NumberOfPlatforms);

        await _stationRepository.AddAsync(station);
    }
}