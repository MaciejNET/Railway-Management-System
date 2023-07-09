using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetPassengersTicketsHandler : IQueryHandler<GetPassengersTickets, IEnumerable<TicketDto>>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetPassengersTicketsHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TicketDto>> HandleAsync(GetPassengersTickets query)
    {
        var passengerId = new UserId(query.PassengerId);
        
        var tickets = await _dbContext.Tickets
            .Where(x => x.PassengerId == passengerId)
            .Include(x => x.Trip)
            .ThenInclude(x => x.Train)
            .ThenInclude(x => x.Carrier)
            .Include(x => x.Stations)
            .Include(x => x.Seat)
            .AsNoTracking()
            .ToListAsync();

        return tickets.Select(x => x.AsDto());
    }
}