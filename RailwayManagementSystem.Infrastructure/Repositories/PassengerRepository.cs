using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class PassengerRepository : GenericRepository<Passenger>, IPassengerRepository
{
    public PassengerRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public new async Task<Passenger?> GetByIdAsync(int id)
    {
        return await _context.Passengers
            .Include(x => x.Discount)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public new async Task<IEnumerable<Passenger>> GetAllAsync() 
        => await _context.Passengers
            .Include(x => x.Discount)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Passenger?> GetByEmailAsync(string email) 
        => await _context.Passengers
            .FirstOrDefaultAsync(x => x.Email == email);

    public async Task<Passenger?> GetByPhoneNumberAsync(string phoneNumber) 
        => await _context.Passengers
            .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
}