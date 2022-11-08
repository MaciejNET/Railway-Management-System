using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

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
        var discount = await _discountRepository.GetById(id);

        if (discount is null)
        {
            var serviceResponse = new ServiceResponse<DiscountDto>
            {
                Success = false,
                Message = "Discount with id: '" + id + "' does not exists."
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
        var discounts = await _discountRepository.GetAll();

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

    public async Task<ServiceResponse<DiscountDto>> AddDiscount(DiscountDto discountDto)
    {
        var discount = await _discountRepository.GetByName(discountDto.Name);
        if (discount is not null)
        {
            var serviceResponse = new ServiceResponse<DiscountDto>
            {
                Success = false,
                Message = "Discount with name :' " + discountDto.Name + "' already exists."
            };

            return serviceResponse;
        }

        discount = new Discount
        {
            Name = discountDto.Name,
            Percentage = discountDto.Percentage
        };
        await _discountRepository.Add(discount);

        var response = new ServiceResponse<DiscountDto>
        {
            Data = _mapper.Map<DiscountDto>(discount)
        };

        return response;
    }

    public async Task<ServiceResponse<DiscountDto>> Delete(int id)
    {
        var discount = await _discountRepository.GetById(id);

        if (discount is null)
        {
            var serviceResponse = new ServiceResponse<DiscountDto>
            {
                Success = false,
                Message = "Discount with id: '" + id + "' does not exists."
            };

            return serviceResponse;
        }

        await _discountRepository.Remove(discount);

        var response = new ServiceResponse<DiscountDto>
        {
            Data = _mapper.Map<DiscountDto>(discount)
        };

        return response;
    }
}