using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface IStationService
{
    Task<ServiceResponse<StationDto>> GetById(int id);
    Task<ServiceResponse<StationDto>> GetByName(string name);
    Task<ServiceResponse<IEnumerable<StationDto>>> GetAll();
    Task<ServiceResponse<IEnumerable<StationDto>>> GetByCity(string city);
    Task<ServiceResponse<StationDto>> AddStation(StationDto stationDto);
    Task<ServiceResponse<StationDto>> Delete(int id);
}