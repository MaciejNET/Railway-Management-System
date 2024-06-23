using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetTicketHandler(RailwayManagementSystemDbContext dbContext) : IQueryHandler<GetTicket, TicketDto>
{
    public async Task<TicketDto> HandleAsync(GetTicket query)
    {
        var ticketId = new TicketId(query.TicketId);

        var ticket = await dbContext.Tickets
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