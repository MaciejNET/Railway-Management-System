using RailwayManagementSystem.Core.Entities;

namespace RailwayManagementSystem.Core.Repositories;

public interface IStationScheduleRepository
{
    Task<bool> IsAnyScheduleInStation(Station station);
}