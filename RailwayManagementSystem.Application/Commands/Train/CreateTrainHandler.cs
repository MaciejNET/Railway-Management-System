using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Train;

public class CreateTrainHandler : ICommandHandler<CreateTrain>
{
    private readonly ITrainRepository _trainRepository;
    private readonly ICarrierRepository _carrierRepository;

    public CreateTrainHandler(ITrainRepository trainRepository, ICarrierRepository carrierRepository)
    {
        _trainRepository = trainRepository;
        _carrierRepository = carrierRepository;
    }

    public async Task HandleAsync(CreateTrain command)
    {
        var trainId = new TrainId(command.Id);
        var name = new TrainName(command.Name);
        var carrierName = new CarrierName(command.CarrierName);
        
        var trainAlreadyExists = await _trainRepository.ExistsByNameAsync(name);

        if (trainAlreadyExists)
        {
            throw new TrainWithGivenNameAlreadyExistsException(name);
        }

        var carrier = await _carrierRepository.GetByNameAsync(carrierName);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(carrierName);
        }

        var train = Core.Entities.Train.Create(trainId, name, command.SeatsAmount, carrier);

        await _trainRepository.AddAsync(train);
    }
}