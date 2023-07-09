using Microsoft.EntityFrameworkCore;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetDiscountsHandler : IQueryHandler<GetDiscounts, IEnumerable<DiscountDto>>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetDiscountsHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<DiscountDto>> HandleAsync(GetDiscounts query)
    {
        var discounts = await _dbContext.Discounts.AsNoTracking().ToListAsync();

        return discounts.Select(x => x.AsDto());
    }
}