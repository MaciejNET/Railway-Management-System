using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;

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
        var trainAlreadyExists = await _trainRepository.ExistsByNameAsync(command.Name);

        if (trainAlreadyExists)
        {
            throw new TrainWithGivenNameAlreadyExistsException(command.Name);
        }

        var carrier = await _carrierRepository.GetByNameAsync(command.CarrierName);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(command.CarrierName);
        }

        var train = Core.Entities.Train.Create(command.Id, command.Name, command.SeatsAmount, carrier);

        await _trainRepository.AddAsync(train);
    }
}