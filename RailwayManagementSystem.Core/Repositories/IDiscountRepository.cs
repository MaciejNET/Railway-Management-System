using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface IDiscountRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Discount?> GetByIdAsync(Guid id);
    Task<Discount?> GetByNameAsync(string name);
    Task AddAsync(Discount discount);
    Task DeleteAsync(Discount discount);
}