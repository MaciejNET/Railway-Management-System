using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface IDiscountRepository : IGenericRepository<Discount>
{
    Task<Discount?> GetByNameAsync(string name);
}