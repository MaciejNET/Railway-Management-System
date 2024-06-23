using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Carrier;

internal sealed class CreateCarrierHandler(ICarrierRepository carrierRepository) : ICommandHandler<CreateCarrier>
{
    public async Task HandleAsync(CreateCarrier command)
    {
        var carrierId = new CarrierId(command.Id);
        var name = new CarrierName(command.Name);
        
        var carrierAlreadyExists = await carrierRepository.ExistsByNameAsync(name);

        if (carrierAlreadyExists)
        {
            throw new CarrierAlreadyExistsException(name);
        }

        var carrier = Core.Entities.Carrier.Create(carrierId, name);
        
        await carrierRepository.AddAsync(carrier);
    }
}