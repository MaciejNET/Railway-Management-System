using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface IPassengerRepository
{
    Task<Passenger?> GetByIdAsync(Guid id);
    Task AddAsync(Passenger passenger);
    Task UpdateAsync(Passenger passenger);
    Task DeleteAsync(Passenger passenger);
}