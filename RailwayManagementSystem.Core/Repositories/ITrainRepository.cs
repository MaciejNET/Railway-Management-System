using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface ITrainRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Train> GetByNameAsync(string name);
    Task AddAsync(Train train);
}