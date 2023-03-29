using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
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

    public async Task<ServiceResponse<DiscountDto>> GetById(int id)
    {
        var discount = await _discountRepository.GetByIdAsync(id);

        if (discount is null)
        {
            var serviceResponse = new ServiceResponse<DiscountDto>
            {
                Success = false,
                Message = $"Discount with id: '{id}' does not exists."
            };

            return serviceResponse;
        }

        var result = new ServiceResponse<DiscountDto>
        {
            Data = _mapper.Map<DiscountDto>(discount)
        };

        return result;
    }

    public async Task<ServiceResponse<IEnumerable<DiscountDto>>> GetAll()
    {
        var discounts = await _discountRepository.GetAllAsync();

        if (!discounts.Any())
        {
            var serviceResponse = new ServiceResponse<IEnumerable<DiscountDto>>
            {
                Success = false,
                Message = "Cannot find any discounts"
            };

            return serviceResponse;
        }

        var result = new ServiceResponse<IEnumerable<DiscountDto>>
        {
            Data = _mapper.Map<IEnumerable<DiscountDto>>(discounts)
        };

        return result;
    }

    public async Task<ServiceResponse<DiscountDto>> AddDiscount(CreateDiscount createDiscount)
    {
        var discount = await _discountRepository.GetByNameAsync(createDiscount.Name);
        if (discount is not null)
        {
            var serviceResponse = new ServiceResponse<DiscountDto>
            {
                Success = false,
                Message = $"Discount with name :'{createDiscount.Name}' already exists."
            };

            return serviceResponse;
        }

        try
        {
            discount = new Discount
            {
                Name = createDiscount.Name,
                Percentage = createDiscount.Percentage
            };
        
            await _discountRepository.AddAsync(discount);
            await _discountRepository.SaveChangesAsync();

            var response = new ServiceResponse<DiscountDto>
            {
                Data = _mapper.Map<DiscountDto>(discount)
            };

            return response;
        }
        catch (Exception e)
        {
            var response = new ServiceResponse<DiscountDto>
            {
                Success = false,
                Message = e.Message
            };
            
            return response;
        }
    }

    public async Task<ServiceResponse<DiscountDto>> Delete(int id)
    {
        var discount = await _discountRepository.GetByIdAsync(id);

        if (discount is null)
        {
            var serviceResponse = new ServiceResponse<DiscountDto>
            {
                Success = false,
                Message = $"Discount with id: '{id}' does not exists."
            };

            return serviceResponse;
        }

        await _discountRepository.RemoveAsync(discount);
        await _discountRepository.SaveChangesAsync();

        var response = new ServiceResponse<DiscountDto>
        {
            Data = _mapper.Map<DiscountDto>(discount)
        };

        return response;
    }
}