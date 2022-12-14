using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface ITripRepository : IGenericRepository<Trip>
{
    Task<IEnumerable<Trip>> GetByTrainId(int id);
}