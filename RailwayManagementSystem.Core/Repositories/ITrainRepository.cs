using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface ITrainRepository : IGenericRepository<Train>
{
    Task<Train?> GetTrainByName(string name);
    Task<IEnumerable<Train>> GetByCarrierId(int id);
}