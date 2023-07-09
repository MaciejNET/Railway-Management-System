using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetCarriersHandler : IQueryHandler<GetCarriers, IEnumerable<CarrierDto>>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetCarriersHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<CarrierDto>> HandleAsync(GetCarriers query)
    {
        var carriers = await _dbContext.Carriers.AsNoTracking().ToListAsync();

        return carriers.Select(x => x.AsDto());
    }
}