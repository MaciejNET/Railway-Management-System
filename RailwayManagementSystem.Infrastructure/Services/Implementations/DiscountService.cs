using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;
using RailwayManagementSystem.Infrastructure.Commands.Discount;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<DiscountDto>> GetById(int id)
    {
        var discount = await _discountRepository.GetByIdAsync(id);

        if (discount is null)
        {
            return Error.NotFound($"Discount with id: '{id}' does not exists.");
        }

        return _mapper.Map<DiscountDto>(discount);
    }

    public async Task<ErrorOr<IEnumerable<DiscountDto>>> GetAll()
    {
        var discounts = await _discountRepository.GetAllAsync();

        if (!discounts.Any())
        {
            return Error.NotFound("Cannot find any discounts");
        }

        var discountsDto = _mapper.Map<IEnumerable<DiscountDto>>(discounts);

        return discountsDto.ToList();
    }

    public async Task<ErrorOr<DiscountDto>> AddDiscount(CreateDiscount createDiscount)
    {
        var discount = await _discountRepository.GetByNameAsync(createDiscount.Name);
        
        if (discount is not null)
        {
            return Error.NotFound($"Discount with name :'{createDiscount.Name}' already exists.");
        }

        ErrorOr<DiscountName> name = DiscountName.Create(createDiscount.Name);

        if (name.IsError)
        {
            return name.Errors;
        }

        discount = Discount.Create(name.Value, createDiscount.Percentage);
        
        await _discountRepository.AddAsync(discount);

        return _mapper.Map<DiscountDto>(discount);
    }

    public async Task<ErrorOr<Deleted>> Delete(int id)
    {
        var discount = await _discountRepository.GetByIdAsync(id);

        if (discount is null)
        {
            return Error.NotFound($"Discount with id: '{id}' does not exists.");
        }

        await _discountRepository.RemoveAsync(discount);
        
        return Result.Deleted;
    }
}