using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Commands.Carrier;

public class CreateCarrierHandler : ICommandHandler<CreateCarrier>
{
    private readonly ICarrierRepository _carrierRepository;

    public CreateCarrierHandler(ICarrierRepository carrierRepository)
    {
        _carrierRepository = carrierRepository;
    }

    public async Task HandleAsync(CreateCarrier command)
    {
        var carrierAlreadyExists = await _carrierRepository.ExistsByNameAsync(command.Name);

        if (carrierAlreadyExists)
        {
            throw new CarrierAlreadyExistsException(command.Name);
        }

        var carrier = Core.Entities.Carrier.Create(command.Id, command.Name);
        
        await _carrierRepository.AddAsync(carrier);
    }
}