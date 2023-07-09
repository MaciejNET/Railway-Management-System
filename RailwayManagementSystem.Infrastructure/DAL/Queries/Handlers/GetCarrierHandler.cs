using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetCarrierHandler : IQueryHandler<GetCarrier, CarrierDto>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetCarrierHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CarrierDto> HandleAsync(GetCarrier query)
    {
        var carrierId = query.Id;

        var carrier = await _dbContext.Carriers.FindAsync(carrierId);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(carrierId);
        }
        
        return carrier.AsDto();
    }
}