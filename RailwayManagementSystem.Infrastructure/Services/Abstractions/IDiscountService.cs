using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Discount;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IDiscountService
{
    Task<ErrorOr<DiscountDto>> GetById(int id);
    Task<ErrorOr<IEnumerable<DiscountDto>>> GetAll();
    Task<ErrorOr<DiscountDto>> AddDiscount(CreateDiscount createDiscount);
    Task<ErrorOr<Deleted>> Delete(int id);
}