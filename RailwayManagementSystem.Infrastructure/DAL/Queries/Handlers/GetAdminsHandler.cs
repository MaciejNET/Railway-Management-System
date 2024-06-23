using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Queries;

namespace RailwayManagementSystem.Infrastructure.DAL.Queries.Handlers;

internal sealed class GetAdminsHandler(RailwayManagementSystemDbContext dbContext)
    : IQueryHandler<GetAdmins, IEnumerable<AdminDto>>
{
    public async Task<IEnumerable<AdminDto>> HandleAsync(GetAdmins query)
    {
        var admins = await dbContext.Admins.AsNoTracking().ToListAsync();

        return admins.Select(x => x.AsDto());
    }
}