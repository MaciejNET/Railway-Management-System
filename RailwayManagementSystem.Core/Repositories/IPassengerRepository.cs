using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface IPassengerRepository
{
    Task<bool> ExistsByEmailAsync(string email);
    Task<Passenger?> GetByIdAsync(Guid id);
    Task<Passenger?> GetByEmailAsync(string email);
    Task AddAsync(Passenger passenger);
    Task UpdateAsync(Passenger passenger);
    Task DeleteAsync(Passenger passenger);
}