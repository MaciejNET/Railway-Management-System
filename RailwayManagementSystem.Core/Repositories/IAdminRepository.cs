using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface IAdminRepository : IGenericRepository<Admin>
{
    Task<Admin?> GetByName(string name);
}