using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface ITrainRepository : IGenericRepository<Train>
{
    Task<Train?> GetTrainByName(string name);
    Task<IEnumerable<Train>> GetByCareerId(int id);
    Task<IEnumerable<Train>> GetByCareerName(string name);
}