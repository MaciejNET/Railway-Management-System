using ErrorOr;
using RailwayManagementSystem.Application.Commands.Station;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IStationService
{
    Task<StationDto> GetById(int id);
    Task<StationDto> GetByName(string name);
    Task<IEnumerable<StationDto>> GetAll();
    Task<IEnumerable<StationDto>> GetByCity(string city);
    Task<StationDto> AddStation(CreateStation createStation);
    Task Delete(int id);
}