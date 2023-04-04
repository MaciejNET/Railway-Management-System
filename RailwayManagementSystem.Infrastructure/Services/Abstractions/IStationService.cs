using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Station;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IStationService
{
    Task<ErrorOr<StationDto>> GetById(int id);
    Task<ErrorOr<StationDto>> GetByName(string name);
    Task<ErrorOr<IEnumerable<StationDto>>> GetAll();
    Task<ErrorOr<IEnumerable<StationDto>>> GetByCity(string city);
    Task<ErrorOr<StationDto>> AddStation(CreateStation createStation);
    Task<ErrorOr<Deleted>> Delete(int id);
}