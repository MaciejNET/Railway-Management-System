using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface ISeatRepository
{
    Task<Seat?> GetByIdAsync(Guid? id);
}