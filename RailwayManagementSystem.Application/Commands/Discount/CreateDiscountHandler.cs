using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Discount;

internal sealed class CreateDiscountHandler(IDiscountRepository discountRepository) : ICommandHandler<CreateDiscount>
{
    public async Task HandleAsync(CreateDiscount command)
    {
        var discountId = new DiscountId(command.Id);
        var name = new DiscountName(command.Name);
        
        var discountAlreadyExists = await discountRepository.ExistsByNameAsync(name);
        
        if (discountAlreadyExists)
        {
            throw new DiscountAlreadyExistsException(name);
        }
        
        var discount = Core.Entities.Discount.Create(discountId, name, command.Percentage);
        
        await discountRepository.AddAsync(discount);
    }
}
