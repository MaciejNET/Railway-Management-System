using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetCarrierTrainsHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetCarrierTrains, IEnumerable<TrainDto>>
{
    public async Task<IEnumerable<TrainDto>> HandleAsync(GetCarrierTrains query)
    {
        var carrierId = new CarrierId(query.Id);

        var carrier = await dbContext.Carriers.FindAsync(carrierId);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(carrierId);
        }

        var trains = await dbContext.Trains.Where(x => x.Carrier == carrier).AsNoTracking().ToListAsync();

        return trains.Select(x => x.AsDto());
    }
}