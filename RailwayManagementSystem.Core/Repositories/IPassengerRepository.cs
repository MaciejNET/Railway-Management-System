using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface IPassengerRepository : IGenericRepository<Passenger>
{
    Task<Passenger?> GetByEmailAsync(string email);
    Task<Passenger?> GetByPhoneNumberAsync(string phoneNumber);
}