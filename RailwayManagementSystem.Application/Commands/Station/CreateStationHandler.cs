using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Station;

internal sealed class CreateStationHandler(IStationRepository stationRepository) : ICommandHandler<CreateStation>
{
    public async Task HandleAsync(CreateStation command)
    {
        var stationId = new StationId(command.Id);
        var name = new StationName(command.Name);
        var city = new City(command.City);
        
        var stationAlreadyExists = await stationRepository.ExistsByNameAsync(name);

        if (stationAlreadyExists)
        {
            throw new StationWithGivenNameAlreadyExistsException(name);
        }

        var station = Core.Entities.Station.Create(stationId, name, city, command.NumberOfPlatforms);

        await stationRepository.AddAsync(station);
    }
}