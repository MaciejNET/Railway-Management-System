using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Passenger;

internal sealed class UpdatePassengerDiscountHandler : ICommandHandler<UpdatePassengerDiscount>
{
    private readonly IPassengerRepository _passengerRepository;
    private readonly IDiscountRepository _discountRepository;

    public UpdatePassengerDiscountHandler(IPassengerRepository passengerRepository, IDiscountRepository discountRepository)
    {
        _passengerRepository = passengerRepository;
        _discountRepository = discountRepository;
    }

    public async Task HandleAsync(UpdatePassengerDiscount command)
    {
        var discountName = new DiscountName(command.DiscountName);
        
        var passenger = await _passengerRepository.GetByIdAsync(command.PassengerId);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(command.PassengerId);
        }

        var discount = await _discountRepository.GetByNameAsync(discountName);

        if (discount is null)
        {
            throw new DiscountNotFoundException(discountName);
        }
        
        passenger.UpdateDiscount(discount);
        await _passengerRepository.UpdateAsync(passenger);
    }
}