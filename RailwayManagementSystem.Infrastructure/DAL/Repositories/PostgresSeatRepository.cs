using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresSeatRepository : ISeatRepository
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public PostgresSeatRepository(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Seat?> GetByIdAsync(Guid? id)
        => await _dbContext.Seats.FindAsync(id);
}