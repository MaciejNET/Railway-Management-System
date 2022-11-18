using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class CarrierRepository : GenericRepository<Carrier>, ICarrierRepository
{
    public CarrierRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<Carrier?> GetByName(string name)
    {
        return await _context.Carriers.FirstOrDefaultAsync(x => x.Name.Value == name);
    }
}