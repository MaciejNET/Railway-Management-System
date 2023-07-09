using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetAdminsHandler : IQueryHandler<GetAdmins, IEnumerable<AdminDto>>
{
    private readonly RailwayManagementSystemDbContext _dbContext;

    public GetAdminsHandler(RailwayManagementSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<AdminDto>> HandleAsync(GetAdmins query)
    {
        var admins = await _dbContext.Admins.AsNoTracking().ToListAsync();

        return admins.Select(x => x.AsDto());
    }
}