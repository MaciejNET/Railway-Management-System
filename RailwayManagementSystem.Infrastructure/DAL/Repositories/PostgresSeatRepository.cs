using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresSeatRepository(RailwayManagementSystemDbContext dbContext) : ISeatRepository
{
    public async Task<Seat?> GetByIdAsync(Guid? id)
        => await dbContext.Seats.FindAsync(id);
}