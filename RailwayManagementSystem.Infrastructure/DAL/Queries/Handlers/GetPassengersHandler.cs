using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetPassengersHandler : IQueryHandler<GetPassengers, IEnumerable<PassengerDto>>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetPassengersHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PassengerDto>> HandleAsync(GetPassengers query)
    {
        var passengers = await _dbContext.Passengers
            .Include(x => x.Discount)
            .AsNoTracking()
            .ToListAsync();

        return passengers.Select(x => x.AsDto());
    }
}