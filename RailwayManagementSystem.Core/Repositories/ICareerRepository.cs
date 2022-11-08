using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface ICareerRepository : IGenericRepository<Career>
{
    Task<Career?> GetByName(string name);
}