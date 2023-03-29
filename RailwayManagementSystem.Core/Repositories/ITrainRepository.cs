using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface ITrainRepository : IGenericRepository<Train>
{
    Task<Train?> GetTrainByNameAsync(string name);
    Task<IEnumerable<Train>> GetByCarrierIdAsync(int id);
}