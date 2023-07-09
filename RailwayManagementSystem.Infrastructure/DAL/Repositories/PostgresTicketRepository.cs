using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresTicketRepository : ITicketRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresTicketRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Ticket?> GetByIdAsync(Guid id)
        => await _dbContext.Tickets.FindAsync(id);

    public async Task DeleteAsync(Ticket ticket)
    {
        _dbContext.Tickets.Remove(ticket);
        await _dbContext.SaveChangesAsync();
    }
}