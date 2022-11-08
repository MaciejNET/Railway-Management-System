using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public class StationService : IStationService
{
    private readonly IMapper _mapper;
    private readonly IStationRepository _stationRepository;

    public StationService(IStationRepository stationRepository, IMapper mapper)
    {
        _stationRepository = stationRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<StationDto>> GetById(int id)
    {
        var station = await _stationRepository.GetById(id);

        if (station is null)
        {
            var serviceResponse = new ServiceResponse<StationDto>
            {
                Success = false,
                Message = "Station with id: '" + id + "' does not exists"
            };

            return serviceResponse;
        }

        var result = new ServiceResponse<StationDto>
        {
            Data = _mapper.Map<StationDto>(station)
        };

        return result;
    }

    public async Task<ServiceResponse<StationDto>> GetByName(string name)
    {
        var station = await _stationRepository.GetByName(name);

        if (station is null)
        {
            var serviceResult = new ServiceResponse<StationDto>
            {
                Success = false,
                Message = $"Station with name: '{name}' does not exists."
            };

            return serviceResult;
        }

        var result = new ServiceResponse<StationDto>
        {
            Data = _mapper.Map<StationDto>(station)
        };

        return result;
    }

    public async Task<ServiceResponse<IEnumerable<StationDto>>> GetAll()
    {
        var stations = await _stationRepository.GetAll();

        if (stations.Any() is false)
        {
            var serviceResult = new ServiceResponse<IEnumerable<StationDto>>
            {
                Success = false,
                Message = "Cannot find any stations."
            };

            return serviceResult;
        }

        var result = new ServiceResponse<IEnumerable<StationDto>>
        {
            Data = _mapper.Map<IEnumerable<StationDto>>(stations)
        };

        return result;
    }

    public async Task<ServiceResponse<IEnumerable<StationDto>>> GetByCity(string city)
    {
        var stations = await _stationRepository.GetByCity(city);
        if (stations.Any() is false)
        {
            var serviceResult = new ServiceResponse<IEnumerable<StationDto>>
            {
                Success = false,
                Message = $"Cannot find any stations for '{city}'."
            };

            return serviceResult;
        }

        var result = new ServiceResponse<IEnumerable<StationDto>>
        {
            Data = _mapper.Map<IEnumerable<StationDto>>(stations)
        };

        return result;
    }

    public async Task<ServiceResponse<StationDto>> AddStation(StationDto stationDto)
    {
        var station = await _stationRepository.GetByName(stationDto.Name);
        if (station is not null)
        {
            var serviceResponse = new ServiceResponse<StationDto>
            {
                Success = false,
                Message = $"Station with '{stationDto.Name}' already exists."
            };

            return serviceResponse;
        }

        station = new Station
        {
            Name = stationDto.Name,
            City = stationDto.City,
            NumberOfPlatforms = stationDto.NumberOfPlatforms
        };
        await _stationRepository.Add(station);

        var response = new ServiceResponse<StationDto>
        {
            Data = _mapper.Map<StationDto>(station)
        };

        return response;
    }

    public async Task<ServiceResponse<StationDto>> Delete(int id)
    {
        var station = await _stationRepository.GetById(id);

        if (station is null)
        {
            var serviceResponse = new ServiceResponse<StationDto>
            {
                Success = false,
                Message = "Station with id: '" + id + "' does not exists"
            };

            return serviceResponse;
        }

        await _stationRepository.Remove(station);

        var result = new ServiceResponse<StationDto>
        {
            Data = _mapper.Map<StationDto>(station)
        };

        return result;
    }
}