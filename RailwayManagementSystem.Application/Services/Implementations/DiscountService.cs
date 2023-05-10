using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.Discount;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Services.Implementations;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _mapper = mapper;
    }

    public async Task<DiscountDto> GetById(int id)
    {
        var discount = await _discountRepository.GetByIdAsync(id);

        if (discount is null)
        {
            throw new DiscountNotFoundException(id);
        }

        return _mapper.Map<DiscountDto>(discount);
    }

    public async Task<IEnumerable<DiscountDto>> GetAll()
    {
        var discounts = await _discountRepository.GetAllAsync();

        if (!discounts.Any())
        {
            return new List<DiscountDto>();
        }

        var discountsDto = _mapper.Map<IEnumerable<DiscountDto>>(discounts);

        return discountsDto.ToList();
    }

    public async Task<DiscountDto> AddDiscount(CreateDiscount createDiscount)
    {
        var discount = await _discountRepository.GetByNameAsync(createDiscount.Name);
        
        if (discount is not null)
        {
            throw new DiscountNotFoundException(createDiscount.Name);
        }

        var name = new DiscountName(createDiscount.Name);

        discount = Discount.Create(name, createDiscount.Percentage);
        
        await _discountRepository.AddAsync(discount);

        return _mapper.Map<DiscountDto>(discount);
    }

    public async Task Delete(int id)
    {
        var discount = await _discountRepository.GetByIdAsync(id);

        if (discount is null)
        {
            throw new DiscountNotFoundException(id);
        }

        await _discountRepository.RemoveAsync(discount);
    }
}