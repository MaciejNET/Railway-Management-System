using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Core.Repositories;

public interface ITripIntervalRepository : IGenericRepository<TripInterval>
{
    Task<TripInterval> GetByTrip(Trip trip);
}