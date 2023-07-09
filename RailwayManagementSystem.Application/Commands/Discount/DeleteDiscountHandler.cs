using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Discount;

internal sealed class DeleteDiscountHandler : ICommandHandler<DeleteDiscount>
{
    private readonly IDiscountRepository _discountRepository;

    public DeleteDiscountHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task HandleAsync(DeleteDiscount command)
    {
        var discountId = new DiscountId(command.Id);

        var discount = await _discountRepository.GetByIdAsync(discountId);

        if (discount is null)
        {
            throw new DiscountNotFoundException(discountId);
        }

        await _discountRepository.DeleteAsync(discount);
    }
}