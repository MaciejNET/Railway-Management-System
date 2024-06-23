using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Passenger;

internal sealed class UpdatePassengerDiscountHandler(
    IPassengerRepository passengerRepository,
    IDiscountRepository discountRepository)
    : ICommandHandler<UpdatePassengerDiscount>
{
    public async Task HandleAsync(UpdatePassengerDiscount command)
    {
        var discountName = new DiscountName(command.DiscountName);
        
        var passenger = await passengerRepository.GetByIdAsync(command.PassengerId);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(command.PassengerId);
        }

        var discount = await discountRepository.GetByNameAsync(discountName);

        if (discount is null)
        {
            throw new DiscountNotFoundException(discountName);
        }
        
        passenger.UpdateDiscount(discount);
        await passengerRepository.UpdateAsync(passenger);
    }
}