using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface ICarrierRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Carrier?> GetByIdAsync(Guid id);
    Task AddAsync(Carrier carrier);
    Task DeleteAsync(Carrier carrier);
}