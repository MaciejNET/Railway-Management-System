using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Train;

internal sealed class DeleteTrainHandler(ITrainRepository trainRepository) : ICommandHandler<DeleteTrain>
{
    public async Task HandleAsync(DeleteTrain command)
    {
        var trainId = new TrainId(command.Id);

        var train = await trainRepository.GetByIdAsync(trainId);

        if (train is null)
        {
            throw new TrainNotFoundException(trainId);
        }

        var isTrainInUse = await trainRepository.IsTrainInUse(train);

        if (isTrainInUse)
        {
            throw new TrainInUseException();
        }

        await trainRepository.DeleteAsync(train);
    }
}