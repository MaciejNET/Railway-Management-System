using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface IStationRepository
{
    Task<bool> ExistsByNameAsync(string name);
    Task<Station> GetByNameAsync(string name);
    Task AddAsync(Station station);
}