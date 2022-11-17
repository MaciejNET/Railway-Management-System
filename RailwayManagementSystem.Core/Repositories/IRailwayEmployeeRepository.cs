using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface IRailwayEmployeeRepository : IGenericRepository<RailwayEmployee>
{
    Task<RailwayEmployee?> GetByName(string name);
}