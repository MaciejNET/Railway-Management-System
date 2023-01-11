using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Discount;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IDiscountService
{
    Task<ServiceResponse<DiscountDto>> GetById(int id);
    Task<ServiceResponse<IEnumerable<DiscountDto>>> GetAll();
    Task<ServiceResponse<DiscountDto>> AddDiscount(CreateDiscount createDiscount);
    Task<ServiceResponse<DiscountDto>> Delete(int id);
}