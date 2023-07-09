using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Carrier;

internal sealed class CreateCarrierHandler : ICommandHandler<CreateCarrier>
{
    private readonly ICarrierRepository _carrierRepository;

    public CreateCarrierHandler(ICarrierRepository carrierRepository)
    {
        _carrierRepository = carrierRepository;
    }

    public async Task HandleAsync(CreateCarrier command)
    {
        var carrierId = new CarrierId(command.Id);
        var name = new CarrierName(command.Name);
        
        var carrierAlreadyExists = await _carrierRepository.ExistsByNameAsync(name);

        if (carrierAlreadyExists)
        {
            throw new CarrierAlreadyExistsException(name);
        }

        var carrier = Core.Entities.Carrier.Create(carrierId, name);
        
        await _carrierRepository.AddAsync(carrier);
    }
}