using ErrorOr;
using RailwayManagementSystem.Application.Commands.Discount;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IDiscountService
{
    Task<DiscountDto> GetById(int id);
    Task<IEnumerable<DiscountDto>> GetAll();
    Task<DiscountDto> AddDiscount(CreateDiscount createDiscount);
    Task Delete(int id);
}