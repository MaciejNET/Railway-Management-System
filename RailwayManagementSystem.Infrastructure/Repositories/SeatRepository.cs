using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Infrastructure.Repositories;

public class SeatRepository : GenericRepository<Seat>, ISeatRepository
{
    public SeatRepository(RailwayManagementSystemDbContext context) : base(context)
    {
    }
}