using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<IEnumerable<Ticket>> GetByTripId(int id);
    Task<IEnumerable<Ticket>> GetByPassengerId(int id);
}