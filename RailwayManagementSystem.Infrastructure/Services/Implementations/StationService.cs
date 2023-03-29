using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
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

    public async Task<ServiceResponse<StationDto>> GetById(int id)
    {
        var station = await _stationRepository.GetByIdAsync(id);

        if (station is null)
        {
            var serviceResponse = new ServiceResponse<StationDto>
            {
                Success = false,
                Message = $"Station with id: '{id}' does not exists"
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
        var station = await _stationRepository.GetByNameAsync(name);

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
        var stations = await _stationRepository.GetAllAsync();

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
        var stations = await _stationRepository.GetByCityAsync(city);
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

    public async Task<ServiceResponse<StationDto>> AddStation(CreateStation createStation)
    {
        var station = await _stationRepository.GetByNameAsync(createStation.Name);
        if (station is not null)
        {
            var serviceResponse = new ServiceResponse<StationDto>
            {
                Success = false,
                Message = $"Station with '{createStation.Name}' already exists."
            };

            return serviceResponse;
        }

        try
        {
            station = new Station
            {
                Name = createStation.Name,
                City = createStation.City,
                NumberOfPlatforms = createStation.NumberOfPlatforms
            };
        
            await _stationRepository.AddAsync(station);
            await _stationRepository.SaveChangesAsync();

            var response = new ServiceResponse<StationDto>
            {
                Data = _mapper.Map<StationDto>(station)
            };

            return response;
        }
        catch (Exception e)
        {
            var response = new ServiceResponse<StationDto>
            {
                Success = false,
                Message = e.Message
            };

            return response;
        }
    }

    public async Task<ServiceResponse<StationDto>> Delete(int id)
    {
        var station = await _stationRepository.GetByIdAsync(id);

        if (station is null)
        {
            var serviceResponse = new ServiceResponse<StationDto>
            {
                Success = false,
                Message = $"Station with id: '{id}' does not exists"
            };

            return serviceResponse;
        }

        await _stationRepository.RemoveAsync(station);
        await _stationRepository.SaveChangesAsync();

        var result = new ServiceResponse<StationDto>
        {
            Data = _mapper.Map<StationDto>(station)
        };

        return result;
    }
}