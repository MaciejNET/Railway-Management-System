using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface IStationRepository : IGenericRepository<Station>
{
    Task<Station?> GetByName(string name);
    Task<IEnumerable<Station>> GetByCity(string city);
}