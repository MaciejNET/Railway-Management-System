using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetDiscountsHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetDiscounts, IEnumerable<DiscountDto>>
{
    public async Task<IEnumerable<DiscountDto>> HandleAsync(GetDiscounts query)
    {
        var discounts = await dbContext.Discounts.AsNoTracking().ToListAsync();

        return discounts.Select(x => x.AsDto());
    }
}