using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.Station;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Services.Implementations;

public class StationService : IStationService
{
    private readonly IMapper _mapper;
    private readonly IStationRepository _stationRepository;

    public StationService(IStationRepository stationRepository, IMapper mapper)
    {
        _stationRepository = stationRepository;
        _mapper = mapper;
    }

    public async Task<StationDto> GetById(int id)
    {
        var station = await _stationRepository.GetByIdAsync(id);

        if (station is null)
        {
            throw new StationNotFoundException(id);
        }

        return _mapper.Map<StationDto>(station);
    }

    public async Task<StationDto> GetByName(string name)
    {
        var station = await _stationRepository.GetByNameAsync(name);

        if (station is null)
        {
            throw new StationNotFoundException(name);
        }

        return _mapper.Map<StationDto>(station);
    }

    public async Task<IEnumerable<StationDto>> GetAll()
    {
        var stations = await _stationRepository.GetAllAsync();

        if (!stations.Any())
        {
            return new List<StationDto>();
        }

        var stationsDto = _mapper.Map<IEnumerable<StationDto>>(stations);
        
        return stationsDto.ToList();
    }

    public async Task<IEnumerable<StationDto>> GetByCity(string city)
    {
        var stations = await _stationRepository.GetByCityAsync(city);
        
        if (!stations.Any())
        {
            return new List<StationDto>();
        }

        var stationsDto = _mapper.Map<IEnumerable<StationDto>>(stations);

        return stationsDto.ToList();
    }

    public async Task<StationDto> AddStation(CreateStation createStation)
    {
        var station = await _stationRepository.GetByNameAsync(createStation.Name);
        
        if (station is not null)
        {
            throw new StationWithGivenNameAlreadyExistsException(createStation.Name);
        }

        var name = new StationName(createStation.Name);
        var city = new City(createStation.City);

        station = Station.Create(name, city, createStation.NumberOfPlatforms);

        await _stationRepository.AddAsync(station);

        return _mapper.Map<StationDto>(station);
    }

    public async Task Delete(int id)
    {
        var station = await _stationRepository.GetByIdAsync(id);

        if (station is null)
        {
            throw new StationNotFoundException(id);
        }

        await _stationRepository.RemoveAsync(station);
    }
}