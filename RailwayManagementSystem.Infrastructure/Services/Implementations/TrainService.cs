using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Models.Enums;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;
using RailwayManagementSystem.Infrastructure.Commands.Train;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

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

    public async Task<ErrorOr<TrainDto>> GetById(int id)
    {
        var train = await _trainRepository.GetByIdAsync(id);

        if (train is null)
        {
            return Error.NotFound($"Train with id: '{id}' does not exists.");
        }

        return _mapper.Map<TrainDto>(train);
    }

    public async Task<ErrorOr<TrainDto>> GetByTrainName(string name)
    {
        var train = await _trainRepository.GetTrainByNameAsync(name);
        
        if (train is null)
        {
            return Error.NotFound($"Train with name: '{name}' does not exists.");
        }

        return _mapper.Map<TrainDto>(train);
    }

    public async Task<ErrorOr<IEnumerable<TrainDto>>> GetByCarrierId(int id)
    {
        var trains = await _trainRepository.GetByCarrierIdAsync(id);

        if (!trains.Any())
        {
            return Error.NotFound($"Cannot find any trains for career with id: {id}.");
        }

        var trainsDto = _mapper.Map<IEnumerable<TrainDto>>(trains);

        return trainsDto.ToList();
    }

    public async Task<ErrorOr<IEnumerable<TrainDto>>> GetAll()
    {
        var trains = await _trainRepository.GetAllAsync();

        if (!trains.Any())
        {
            return Error.NotFound("Cannot find any trains");
        }

        var trainsDto = _mapper.Map<IEnumerable<TrainDto>>(trains);

        return trainsDto.ToList();
    }

    public async Task<ErrorOr<TrainDto>> AddTrain(CreateTrain createTrain)
    {
        var train = await _trainRepository.GetTrainByNameAsync(createTrain.Name);

        if (train is not null)
        {
            return Error.Validation($"Train with name: '{createTrain.Name}' already exists.");
        }

        var carrier = await _carrierRepository.GetByNameAsync(createTrain.CarrierName);

        if (carrier is null)
        {
            return Error.Validation(
                $"Cannot create train because carrier with name: '{createTrain.CarrierName}' does not exists.");
        }

        ErrorOr<TrainName> name = TrainName.Create(createTrain.Name);

        if (name.IsError)
        {
            return name.Errors;
        }

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

    public async Task<ErrorOr<Deleted>> Delete(int id)
    {
        var train = await _trainRepository.GetByIdAsync(id);

        if (train is null)
        {
            return Error.NotFound($"Train with id: '{id}' does not exists.");
        }

        await _trainRepository.RemoveAsync(train);

        return Result.Deleted;
    }
}