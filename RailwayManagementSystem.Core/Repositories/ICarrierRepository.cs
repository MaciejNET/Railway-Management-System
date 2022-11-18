using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface ICarrierRepository : IGenericRepository<Carrier>
{
    Task<Carrier?> GetByName(string name);
}