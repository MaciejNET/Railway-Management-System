using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface ICarrierRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Carrier> GetByNameAsync(string name);
    Task AddAsync(Carrier carrier);
}