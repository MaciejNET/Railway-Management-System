using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Train;

internal sealed class CreateTrainHandler(ITrainRepository trainRepository, ICarrierRepository carrierRepository)
    : ICommandHandler<CreateTrain>
{
    public async Task HandleAsync(CreateTrain command)
    {
        var trainId = new TrainId(command.Id);
        var name = new TrainName(command.Name);
        var carrierId = new CarrierId(command.CarrierId);
        
        var trainAlreadyExists = await trainRepository.ExistsByNameAsync(name);

        if (trainAlreadyExists)
        {
            throw new TrainWithGivenNameAlreadyExistsException(name);
        }

        var carrier = await carrierRepository.GetByIdAsync(carrierId);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(carrierId);
        }

        var train = Core.Entities.Train.Create(trainId, name, command.SeatsAmount, carrier);
        carrier.AddTrain(train);

        await trainRepository.AddAsync(train);
    }
}