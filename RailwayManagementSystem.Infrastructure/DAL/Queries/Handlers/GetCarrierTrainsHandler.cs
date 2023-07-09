using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetCarrierTrainsHandler : IQueryHandler<GetCarrierTrains, IEnumerable<TrainDto>>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetCarrierTrainsHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TrainDto>> HandleAsync(GetCarrierTrains query)
    {
        var carrierId = new CarrierId(query.Id);

        var carrier = await _dbContext.Carriers.FindAsync(carrierId);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(carrierId);
        }

        var trains = await _dbContext.Trains.Where(x => x.Carrier == carrier).AsNoTracking().ToListAsync();

        return trains.Select(x => x.AsDto());
    }
}