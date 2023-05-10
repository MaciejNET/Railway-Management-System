using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.Trip;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Extensions;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Services.Implementations;

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

    public async Task<TripDto> GetById(int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);

        if (trip is null)
        {
            throw new TripNotFoundException(id);
        }

        return _mapper.Map<TripDto>(trip);
    }

    public async Task<IEnumerable<TripDto>> GetAll()
    {
        var trips = await _tripRepository.GetAllAsync();

        if (!trips.Any())
        {
            return new List<TripDto>();
        }

        var tripsDto = _mapper.Map<IEnumerable<TripDto>>(trips);

        return tripsDto.ToList();
    }

    public async Task<IEnumerable<ConnectionTripDto>> GetConnectionTrip(string startStation,
        string endStation, DateTime date)
    {
        var start = await _stationRepository.GetByNameAsync(startStation);

        if (start is null)
        {
            throw new StationNotFoundException(startStation);
        }

        var end = await _stationRepository.GetByNameAsync(endStation);

        if (end is null)
        {
            throw new StationNotFoundException(endStation);
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

    public async Task<TripDto> AddTrip(CreateTrip createTrip)
    {
        var train = await _trainRepository.GetTrainByNameAsync(createTrip.TrainName);

        if (train is null)
        {
            throw new TrainNotFoundException(createTrip.TrainName);
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
                throw new StationNotFoundException(scheduleDto.StationName);
            }

            if (scheduleDto.DepartureTime < scheduleDto.ArrivalTime)
            {
                throw new InvalidDepartureTimeException(scheduleDto.StationName);
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

    public async Task Delete(int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);

        if (trip is null)
        {
            throw new TripNotFoundException(id);
        }

        await _tripRepository.RemoveAsync(trip);
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