using ErrorOr;
using RailwayManagementSystem.Application.Commands.Station;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IStationService
{
    Task<ErrorOr<StationDto>> GetById(int id);
    Task<ErrorOr<StationDto>> GetByName(string name);
    Task<ErrorOr<IEnumerable<StationDto>>> GetAll();
    Task<ErrorOr<IEnumerable<StationDto>>> GetByCity(string city);
    Task<ErrorOr<StationDto>> AddStation(CreateStation createStation);
    Task<ErrorOr<Deleted>> Delete(int id);
}