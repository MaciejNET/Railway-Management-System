using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal class GetPassengerHandler : IQueryHandler<GetPassenger, PassengerDto>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetPassengerHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PassengerDto> HandleAsync(GetPassenger query)
    {
        var passengerId = new UserId(query.Id);
        
        var passenger = await _dbContext.Passengers
            .Include(x => x.Discount)
            .SingleOrDefaultAsync(x => x.Id == passengerId);

        if (passenger is null)
        {
            throw new PassengerNotFoundException(passengerId);
        }

        return passenger.AsDto();
    }
}