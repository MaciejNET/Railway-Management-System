using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.DAL.Repositories;

internal sealed class PostgresPassengerRepository(RailwayManagementSystemDbContext dbContext) : IPassengerRepository
{
    public Task<bool> ExistsByEmailAsync(string email)
        => dbContext.Passengers.AnyAsync(x => x.Email == email);

    public async Task<Passenger?> GetByIdAsync(Guid id)
        => await dbContext.Passengers.FindAsync(id);

    public Task<Passenger?> GetByEmailAsync(string email)
        => dbContext.Passengers.SingleOrDefaultAsync(x => x.Email == email);

    public async Task AddAsync(Passenger passenger)
    {
        await dbContext.Passengers.AddAsync(passenger);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Passenger passenger)
    {
        dbContext.Passengers.Update(passenger);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Passenger passenger)
    {
        dbContext.Passengers.Remove(passenger);
        await dbContext.SaveChangesAsync();
    }
}