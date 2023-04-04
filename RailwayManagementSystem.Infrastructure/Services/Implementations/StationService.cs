using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;
using RailwayManagementSystem.Infrastructure.Commands.Station;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

public class StationService : IStationService
{
    private readonly IMapper _mapper;
    private readonly IStationRepository _stationRepository;

    public StationService(IStationRepository stationRepository, IMapper mapper)
    {
        _stationRepository = stationRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<StationDto>> GetById(int id)
    {
        var station = await _stationRepository.GetByIdAsync(id);

        if (station is null)
        {
            return Error.NotFound($"Station with id: '{id}' does not exists");
        }

        return _mapper.Map<StationDto>(station);
    }

    public async Task<ErrorOr<StationDto>> GetByName(string name)
    {
        var station = await _stationRepository.GetByNameAsync(name);

        if (station is null)
        {
            return Error.NotFound($"Station with name: '{name}' does not exists.");
        }

        return _mapper.Map<StationDto>(station);
    }

    public async Task<ErrorOr<IEnumerable<StationDto>>> GetAll()
    {
        var stations = await _stationRepository.GetAllAsync();

        if (!stations.Any())
        {
            return Error.NotFound("Cannot find any stations.");
        }

        var stationsDto = _mapper.Map<IEnumerable<StationDto>>(stations);
        
        return stationsDto.ToList();
    }

    public async Task<ErrorOr<IEnumerable<StationDto>>> GetByCity(string city)
    {
        var stations = await _stationRepository.GetByCityAsync(city);
        
        if (!stations.Any())
        {
            return Error.NotFound($"Cannot find any stations for '{city}'.");
        }

        var stationsDto = _mapper.Map<IEnumerable<StationDto>>(stations);

        return stationsDto.ToList();
    }

    public async Task<ErrorOr<StationDto>> AddStation(CreateStation createStation)
    {
        var station = await _stationRepository.GetByNameAsync(createStation.Name);
        
        if (station is not null)
        {
            return Error.Validation($"Station with '{createStation.Name}' already exists.");
        }

        ErrorOr<StationName> name = StationName.Create(createStation.Name);
        ErrorOr<City> city = City.Create(createStation.City);

        if (name.IsError || city.IsError)
        {
            List<Error> errors = new();
            
            if (name.IsError) errors.AddRange(name.Errors);
            if (city.IsError) errors.AddRange(city.Errors);

            return errors;
        }

        station = Station.Create(name.Value, city.Value, createStation.NumberOfPlatforms);

        await _stationRepository.AddAsync(station);

        return _mapper.Map<StationDto>(station);
    }

    public async Task<ErrorOr<Deleted>> Delete(int id)
    {
        var station = await _stationRepository.GetByIdAsync(id);

        if (station is null)
        {
            return Error.NotFound($"Station with id: '{id}' does not exists");
        }

        await _stationRepository.RemoveAsync(station);

        return Result.Deleted;
    }
}