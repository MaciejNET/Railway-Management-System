using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetCarriersHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetCarriers, IEnumerable<CarrierDto>>
{
    public async Task<IEnumerable<CarrierDto>> HandleAsync(GetCarriers query)
    {
        var carriers = await dbContext.Carriers.AsNoTracking().ToListAsync();

        return carriers.Select(x => x.AsDto());
    }
}