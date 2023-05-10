using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.Train;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Services.Implementations;

public class TrainService : ITrainService
{
    private readonly ICarrierRepository _carrierRepository;
    private readonly IMapper _mapper;
    private readonly ISeatRepository _seatRepository;
    private readonly ITrainRepository _trainRepository;

    public TrainService(ITrainRepository trainRepository, ISeatRepository seatRepository,
        ICarrierRepository carrierRepository, IMapper mapper)
    {
        _trainRepository = trainRepository;
        _seatRepository = seatRepository;
        _carrierRepository = carrierRepository;
        _mapper = mapper;
    }

    public async Task<TrainDto> GetById(int id)
    {
        var train = await _trainRepository.GetByIdAsync(id);

        if (train is null)
        {
            throw new TrainNotFoundException(id);
        }

        return _mapper.Map<TrainDto>(train);
    }

    public async Task<TrainDto> GetByTrainName(string name)
    {
        var train = await _trainRepository.GetTrainByNameAsync(name);
        
        if (train is null)
        {
            throw new TrainNotFoundException(name);
        }

        return _mapper.Map<TrainDto>(train);
    }

    public async Task<IEnumerable<TrainDto>> GetByCarrierId(int id)
    {
        var trains = await _trainRepository.GetByCarrierIdAsync(id);

        if (!trains.Any())
        {
            return new List<TrainDto>();
        }

        var trainsDto = _mapper.Map<IEnumerable<TrainDto>>(trains);

        return trainsDto.ToList();
    }

    public async Task<IEnumerable<TrainDto>> GetAll()
    {
        var trains = await _trainRepository.GetAllAsync();

        if (!trains.Any())
        {
            return new List<TrainDto>();
        }

        var trainsDto = _mapper.Map<IEnumerable<TrainDto>>(trains);

        return trainsDto.ToList();
    }

    public async Task<TrainDto> AddTrain(CreateTrain createTrain)
    {
        var train = await _trainRepository.GetTrainByNameAsync(createTrain.Name);

        if (train is not null)
        {
            throw new TrainWithGivenNameAlreadyExistsException(createTrain.Name);
        }

        var carrier = await _carrierRepository.GetByNameAsync(createTrain.CarrierName);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(createTrain.CarrierName);
        }

        var name = new TrainName(createTrain.Name);

        train = Train.Create(name.Value, createTrain.SeatsAmount, carrier);
        await _trainRepository.AddAsync(train);
        var seats = new List<Seat>();
        for (var i = 0; i < train.SeatsAmount; i++)
            seats.Add(
                Seat.Create(
                    seatNumber: i + 1,
                    place: (i + 1) % 2 == 0 ? Place.Window : Place.Middle,
                    train: train));

        await _seatRepository.AddRangeAsync(seats);

        return _mapper.Map<TrainDto>(train);
    }

    public async Task Delete(int id)
    {
        var train = await _trainRepository.GetByIdAsync(id);

        if (train is null)
        {
            throw new TrainNotFoundException(id);
        }

        await _trainRepository.RemoveAsync(train);
    }
}