using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;
using RailwayManagementSystem.Infrastructure.Commands.Trip;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Extensions;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

public class TripService : ITripService
{
    private readonly IMapper _mapper;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IStationRepository _stationRepository;
    private readonly ITrainRepository _trainRepository;
    private readonly ITripRepository _tripRepository;

    public TripService(
        ITripRepository tripRepository,
        ITrainRepository trainRepository,
        IStationRepository stationRepository,
        IScheduleRepository scheduleRepository,
        IMapper mapper)
    {
        _tripRepository = tripRepository;
        _trainRepository = trainRepository;
        _stationRepository = stationRepository;
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TripDto>> GetById(int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);

        if (trip is null)
        {
            return Error.NotFound($"Trip with id: '{id}' dose not exists");
        }

        return _mapper.Map<TripDto>(trip);
    }

    public async Task<ErrorOr<IEnumerable<TripDto>>> GetAll()
    {
        var trips = await _tripRepository.GetAllAsync();

        if (!trips.Any())
        {
            return Error.NotFound("Cannot find any trip");
        }

        var tripsDto = _mapper.Map<IEnumerable<TripDto>>(trips);

        return tripsDto.ToList();
    }

    public async Task<ErrorOr<IEnumerable<ConnectionTripDto>>> GetConnectionTrip(string startStation,
        string endStation, DateTime date)
    {
        var start = await _stationRepository.GetByNameAsync(startStation);

        if (start is null)
        {
            return Error.NotFound($"Station with name: '{startStation}' does not exists.");
        }

        var end = await _stationRepository.GetByNameAsync(endStation);

        if (end is null)
        {
            return Error.NotFound($"Station with name: '{endStation}' does not exists.");
        }

        var trips = await GetAllTrips(start, date);

        var connectionTrips = (from trip in trips
            where trip.Schedules.Select(x => x.Station).Contains(end)
            select new ConnectionTripDto
            {
                TrainName = trip.Train.Name,
                StartStation = startStation,
                EndStation = endStation,
                DepartureTime = trip.Schedules.FirstOrDefault(x => x.Station.Equals(start)).DepartureTime,
                ArrivalTime = trip.Schedules.FirstOrDefault(x => x.Station.Equals(end)).ArrivalTime
            }).ToList();

        return connectionTrips;
    }

    public async Task<ErrorOr<TripDto>> AddTrip(CreateTrip createTrip)
    {
        var train = await _trainRepository.GetTrainByNameAsync(createTrip.TrainName);

        if (train is null)
        {
            return Error.NotFound($"Train with name: '{createTrip.TrainName}' does not exists.");
        }

        var tripInterval = new TripInterval(
            createTrip.TripIntervalDto.Monday,
            createTrip.TripIntervalDto.Tuesday,
            createTrip.TripIntervalDto.Wednesday,
            createTrip.TripIntervalDto.Thursday,
            createTrip.TripIntervalDto.Friday,
            createTrip.TripIntervalDto.Saturday,
            createTrip.TripIntervalDto.Sunday);


        var trip = Trip.Create(createTrip.Price, train, tripInterval);
        var schedules = new List<Schedule>();
        foreach (var scheduleDto in createTrip.ScheduleDtos)
        {
            var station = await _stationRepository.GetByNameAsync(scheduleDto.StationName);
                
            if (station is null)
            {
                return Error.NotFound($"Station with name: '{scheduleDto.StationName}' does not exists.");
            }

            if (scheduleDto.DepartureTime < scheduleDto.ArrivalTime)
            {
                return Error.Conflict(
                    $"In station: '{scheduleDto.StationName}' departure time is before arrival time.");
            }

            schedules.Add(
                Schedule.Create(
                    trip,
                    station,
                    scheduleDto.ArrivalTime,
                    scheduleDto.DepartureTime,
                    scheduleDto.Platform));
        }
        
        await _tripRepository.AddAsync(trip);
        await _scheduleRepository.AddRangeAsync(schedules);

        return _mapper.Map<TripDto>(trip);
    }

    public async Task<ErrorOr<Deleted>> Delete(int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);

        if (trip is null)
        {
            return Error.NotFound($"Trip with id: '{id}' dose not exists");
        }

        await _tripRepository.RemoveAsync(trip);

        return Result.Deleted;
    }

    private async Task<IEnumerable<Trip>> GetAllTrips(Station station, DateTime tripDate)
    {
        var time = TimeOnly.FromDateTime(tripDate);
        var date = DateOnly.FromDateTime(tripDate);
        var schedules = await _scheduleRepository.GetByDepartureTimeAndStationIdAsync(time, station.Id);
        var trips = schedules.Select(x => x.Trip).Where(x => TripExtensions.IsTrainRunsOnGivenDate(x, date)).ToList();
        
        return trips;
    }
}