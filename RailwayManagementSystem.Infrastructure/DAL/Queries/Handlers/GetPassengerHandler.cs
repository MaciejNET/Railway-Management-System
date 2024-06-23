using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal class GetPassengerHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetPassenger, PassengerDto>
{
    public async Task<PassengerDto> HandleAsync(GetPassenger query)
    {
        var passengerId = new UserId(query.Id);
        
        var passenger = await dbContext.Passengers
            .Include(x => x.Discount)
            .SingleOrDefaultAsync(x => x.Id == passengerId);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(passengerId);
        }

        return passenger.AsDto();
    }
}