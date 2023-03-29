using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface IStationRepository : IGenericRepository<Station>
{
    Task<Station?> GetByNameAsync(string name);
    Task<IEnumerable<Station>> GetByCityAsync(string city);
}