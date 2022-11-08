using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface IPassengerRepository : IGenericRepository<Passenger>
{
    Task<Passenger?> GetByEmail(string email);
    Task<Passenger?> GetByPhoneNumber(string phoneNumber);
}