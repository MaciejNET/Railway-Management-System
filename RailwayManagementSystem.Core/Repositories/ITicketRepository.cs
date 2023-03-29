using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetByTripIdAsync(int id);
    Task<IEnumerable<Ticket>> GetByPassengerIdAsync(int id);
}