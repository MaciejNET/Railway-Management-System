using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Handlers;

internal sealed class GetPassengersTicketsHandler : IQueryHandler<GetPassengersTickets, IEnumerable<TicketDto>>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetPassengersTicketsHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TicketDto>> HandleAsync(GetPassengersTickets query)
    {
        var tickets = await _dbContext.Tickets
            .Where(x => x.PassengerId.Equals(query.PassengerId))
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