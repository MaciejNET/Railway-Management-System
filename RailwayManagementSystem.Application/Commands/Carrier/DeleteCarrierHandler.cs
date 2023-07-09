using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Carrier;

internal sealed class DeleteCarrierHandler : ICommandHandler<DeleteCarrier>
{
    private readonly ICarrierRepository _carrierRepository;

    public DeleteCarrierHandler(ICarrierRepository carrierRepository)
    {
        _carrierRepository = carrierRepository;
    }

    public async Task HandleAsync(DeleteCarrier command)
    {
        var carrierId = new CarrierId(command.Id);

        var carrier = await _carrierRepository.GetByIdAsync(carrierId);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(carrierId);
        }

        await _carrierRepository.DeleteAsync(carrier);
    }
}