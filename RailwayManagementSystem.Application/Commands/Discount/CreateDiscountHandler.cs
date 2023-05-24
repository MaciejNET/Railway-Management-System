using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Discount;

public class CreateDiscountHandler : ICommandHandler<CreateDiscount>
{
    private readonly IDiscountRepository _discountRepository;
   
    public CreateDiscountHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }

    public async Task HandleAsync(CreateDiscount command)
    {
        var discountAlreadyExists = await _discountRepository.ExistsByNameAsync(command.Name);
        
        if (discountAlreadyExists)
        {
            throw new DiscountAlreadyExistsException(command.Name);
        }
        
        var discount = Core.Entities.Discount.Create(command.Id, command.Name, command.Percentage);
        
        await _discountRepository.AddAsync(discount);
    }
}
