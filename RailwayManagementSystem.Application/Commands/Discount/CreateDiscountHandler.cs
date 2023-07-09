using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Discount;

internal sealed class CreateDiscountHandler : ICommandHandler<CreateDiscount>
{
    private readonly IDiscountRepository _discountRepository;
   
    public CreateDiscountHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task HandleAsync(CreateDiscount command)
    {
        var discountId = new DiscountId(command.Id);
        var name = new DiscountName(command.Name);
        
        var discountAlreadyExists = await _discountRepository.ExistsByNameAsync(name);
        
        if (discountAlreadyExists)
        {
            throw new DiscountAlreadyExistsException(name);
        }
        
        var discount = Core.Entities.Discount.Create(discountId, name, command.Percentage);
        
        await _discountRepository.AddAsync(discount);
    }
}
