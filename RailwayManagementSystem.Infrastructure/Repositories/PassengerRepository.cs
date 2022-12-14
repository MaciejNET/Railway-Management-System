using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class PassengerRepository : GenericRepository<Passenger>, IPassengerRepository
{
    public PassengerRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public new async Task<Passenger?> GetById(int id)
    {
        return await _context.Passengers.Include(x => x.Discount).FirstOrDefaultAsync(x => x.Id == id);
    }

    public new async Task<IEnumerable<Passenger>> GetAll()
    {
        return await _context.Passengers.Include(x => x.Discount).ToListAsync();
    }

    public async Task<Passenger?> GetByEmail(string email)
    {
        return await _context.Passengers.FirstOrDefaultAsync(x => x.Email.Value == email);
    }

    public async Task<Passenger?> GetByPhoneNumber(string phoneNumber)
    {
        return await _context.Passengers.FirstOrDefaultAsync(x => x.PhoneNumber.Value == phoneNumber);
    }
}