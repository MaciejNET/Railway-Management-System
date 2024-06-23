using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetCarrierHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetCarrier, CarrierDto>
{
    public async Task<CarrierDto> HandleAsync(GetCarrier query)
    {
        var carrierId = query.Id;

        var carrier = await dbContext.Carriers.FindAsync(carrierId);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(carrierId);
        }
        
        return carrier.AsDto();
    }
}