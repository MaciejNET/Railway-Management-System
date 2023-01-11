using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class RailwayEmployeeRepository : GenericRepository<RailwayEmployee>, IRailwayEmployeeRepository
{
    public RailwayEmployeeRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }

    public async Task<RailwayEmployee?> GetByName(string name) 
        => await _context.RailwayEmployees.FirstOrDefaultAsync(x => x.Name.Value == name);
}