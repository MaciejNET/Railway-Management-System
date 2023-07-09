using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface ITrainRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<bool> IsTrainInUse(Train train);
    Task<Train?> GetByIdAsync(Guid id);
    Task<Train?> GetByNameAsync(string name);
    Task AddAsync(Train train);
    Task DeleteAsync(Train train);
}