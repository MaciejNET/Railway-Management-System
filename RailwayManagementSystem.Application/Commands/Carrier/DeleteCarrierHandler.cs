using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Carrier;

internal sealed class DeleteCarrierHandler(ICarrierRepository carrierRepository) : ICommandHandler<DeleteCarrier>
{
    public async Task HandleAsync(DeleteCarrier command)
    {
        var carrierId = new CarrierId(command.Id);

        var carrier = await carrierRepository.GetByIdAsync(carrierId);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(carrierId);
        }

        await carrierRepository.DeleteAsync(carrier);
    }
}