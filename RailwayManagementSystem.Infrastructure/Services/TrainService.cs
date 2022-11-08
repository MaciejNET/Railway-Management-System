using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public class TrainService : ITrainService
{
    private readonly ICareerRepository _careerRepository;
    private readonly IMapper _mapper;
    private readonly ISeatRepository _seatRepository;
    private readonly ITrainRepository _trainRepository;

    public TrainService(ITrainRepository trainRepository, ISeatRepository seatRepository,
        ICareerRepository careerRepository, IMapper mapper)
    {
        _trainRepository = trainRepository;
        _seatRepository = seatRepository;
        _careerRepository = careerRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<TrainDto>> GetById(int id)
    {
        var train = await _trainRepository.GetById(id);

        if (train is null)
        {
            var serviceResponse = new ServiceResponse<TrainDto>
            {
                Success = false,
                Message = $"Train with id: '{id}' does not exists."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<TrainDto>
        {
            Data = _mapper.Map<TrainDto>(train)
        };

        return response;
    }

    public async Task<ServiceResponse<TrainDto>> GetByTrainName(string name)
    {
        var train = await _trainRepository.GetTrainByName(name);
        if (train is null)
        {
            var serviceResponse = new ServiceResponse<TrainDto>
            {
                Success = false,
                Message = $"Train with name: '{name}' does not exists."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<TrainDto>
        {
            Data = _mapper.Map<TrainDto>(train)
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<TrainDto>>> GetByCareerId(int id)
    {
        var trains = await _trainRepository.GetByCareerId(id);

        if (trains.Any() is false)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<TrainDto>>
            {
                Success = false,
                Message = $"Cannot find any trains for career with id: {id}."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<IEnumerable<TrainDto>>
        {
            Data = _mapper.Map<IEnumerable<TrainDto>>(trains)
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<TrainDto>>> GetAll()
    {
        var trains = await _trainRepository.GetAll();

        if (trains.Any() is false)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<TrainDto>>
            {
                Success = false,
                Message = "Cannot find any trains"
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<IEnumerable<TrainDto>>
        {
            Data = _mapper.Map<IEnumerable<TrainDto>>(trains)
        };

        return response;
    }

    public async Task<ServiceResponse<TrainDto>> AddTrain(TrainDto trainDto)
    {
        var train = await _trainRepository.GetTrainByName(trainDto.Name);

        if (train is not null)
        {
            var serviceResponse = new ServiceResponse<TrainDto>
            {
                Success = false,
                Message = $"Train with name: '{trainDto.Name}' already exists."
            };

            return serviceResponse;
        }

        var career = await _careerRepository.GetByName(trainDto.CareerName);

        if (career is null)
        {
            var serviceResponse = new ServiceResponse<TrainDto>
            {
                Success = false,
                Message = $"Cannot create train because career with name: '{trainDto.Name}' does not exists."
            };

            return serviceResponse;
        }

        train = new Train
        {
            Name = trainDto.Name,
            SeatsAmount = trainDto.SeatsAmount,
            Career = career
        };
        await _trainRepository.Add(train);
        var seats = new List<Seat>();
        for (var i = 0; i < train.SeatsAmount; i++)
            seats.Add(new Seat
            {
                SeatNumber = i + 1,
                Place = (i + 1) % 2 == 0 ? Place.Window : Place.Middle,
                Train = train
            });

        await _seatRepository.AddRange(seats);

        var response = new ServiceResponse<TrainDto>
        {
            Data = _mapper.Map<TrainDto>(train)
        };

        return response;
    }

    public async Task<ServiceResponse<TrainDto>> Delete(int id)
    {
        var train = await _trainRepository.GetById(id);

        if (train is null)
        {
            var serviceResponse = new ServiceResponse<TrainDto>
            {
                Success = false,
                Message = $"Train with id: '{id}' does not exists."
            };

            return serviceResponse;
        }

        await _trainRepository.Remove(train);

        var response = new ServiceResponse<TrainDto>
        {
            Data = _mapper.Map<TrainDto>(train)
        };

        return response;
    }
}