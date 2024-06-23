using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetPassengersHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetPassengers, IEnumerable<PassengerDto>>
{
    public async Task<IEnumerable<PassengerDto>> HandleAsync(GetPassengers query)
    {
        var passengers = await dbContext.Passengers
            .Include(x => x.Discount)
            .AsNoTracking()
            .ToListAsync();

        return passengers.Select(x => x.AsDto());
    }
}