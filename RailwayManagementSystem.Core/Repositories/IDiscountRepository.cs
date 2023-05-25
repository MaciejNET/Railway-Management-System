using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface IDiscountRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Discount?> GetByNameAsync(string name);
    Task AddAsync(Discount discount);
}