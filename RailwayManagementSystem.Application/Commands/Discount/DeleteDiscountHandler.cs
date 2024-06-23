using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Discount;

internal sealed class DeleteDiscountHandler(IDiscountRepository discountRepository) : ICommandHandler<DeleteDiscount>
{
    public async Task HandleAsync(DeleteDiscount command)
    {
        var discountId = new DiscountId(command.Id);

        var discount = await discountRepository.GetByIdAsync(discountId);

        if (discount is null)
        {
            throw new DiscountNotFoundException(discountId);
        }

        await discountRepository.DeleteAsync(discount);
    }
}