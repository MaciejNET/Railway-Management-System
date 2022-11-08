using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface IDiscountService
{
    Task<ServiceResponse<DiscountDto>> GetById(int id);
    Task<ServiceResponse<IEnumerable<DiscountDto>>> GetAll();
    Task<ServiceResponse<DiscountDto>> AddDiscount(DiscountDto discountDto);
    Task<ServiceResponse<DiscountDto>> Delete(int id);
}