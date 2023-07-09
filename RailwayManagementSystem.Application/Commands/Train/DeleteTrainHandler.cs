using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Train;

internal sealed class DeleteTrainHandler : ICommandHandler<DeleteTrain>
{
    private readonly ITrainRepository _trainRepository;

    public DeleteTrainHandler(ITrainRepository trainRepository)
    {
        _trainRepository = trainRepository;
    }

    public async Task HandleAsync(DeleteTrain command)
    {
        var trainId = new TrainId(command.Id);

        var train = await _trainRepository.GetByIdAsync(trainId);

        if (train is null)
        {
            throw new TrainNotFoundException(trainId);
        }

        var isTrainInUse = await _trainRepository.IsTrainInUse(train);

        if (isTrainInUse)
        {
            throw new TrainInUseException();
        }

        await _trainRepository.DeleteAsync(train);
    }
}