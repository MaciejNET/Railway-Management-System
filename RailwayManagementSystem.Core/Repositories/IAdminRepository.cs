using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface IAdminRepository
{
    Task<bool> ExistByNameAsync(string name);
    Task<Admin?> GetByNameAsync(string name);
    Task AddAsync(Admin admin);
}