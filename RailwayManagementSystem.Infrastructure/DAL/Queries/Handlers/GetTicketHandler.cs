using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetTicketHandler : IQueryHandler<GetTicket, TicketDto>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetTicketHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TicketDto> HandleAsync(GetTicket query)
    {
        var ticketId = new TicketId(query.TicketId);

        var ticket = await _dbContext.Tickets
            .Include(x => x.Stations)
            .Include(x => x.Seat)
            .Include(x => x.Trip)
            .ThenInclude(x => x.Train)
            .ThenInclude(x => x.Carrier)
            .SingleOrDefaultAsync(x => x.Id == ticketId);

        if (ticket is null)
        {
            throw new TicketNotFoundException(ticketId);
        }

        return ticket.AsDto();
    }
}