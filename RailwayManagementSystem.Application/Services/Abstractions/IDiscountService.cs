using ErrorOr;
using RailwayManagementSystem.Application.Commands.Discount;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IDiscountService
{
    Task<ErrorOr<DiscountDto>> GetById(int id);
    Task<ErrorOr<IEnumerable<DiscountDto>>> GetAll();
    Task<ErrorOr<DiscountDto>> AddDiscount(CreateDiscount createDiscount);
    Task<ErrorOr<Deleted>> Delete(int id);
}