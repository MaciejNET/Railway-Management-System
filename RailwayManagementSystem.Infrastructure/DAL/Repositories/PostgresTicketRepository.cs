using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresTicketRepository(RailwayManagementSystemDbContext dbContext) : ITicketRepository
{
    public async Task<Ticket?> GetByIdAsync(Guid id)
        => await dbContext.Tickets.FindAsync(id);

    public async Task DeleteAsync(Ticket ticket)
    {
        dbContext.Tickets.Remove(ticket);
        await dbContext.SaveChangesAsync();
    }
}