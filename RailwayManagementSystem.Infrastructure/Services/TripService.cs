using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.Commands.Trip;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Extensions;

namespace RailwayManagementSystem.Infrastructure.Services;

public class TripService : ITripService
{
    private readonly IMapper _mapper;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IStationRepository _stationRepository;
    private readonly ITrainRepository _trainRepository;
    private readonly ITripIntervalRepository _tripIntervalRepository;
    private readonly ITripRepository _tripRepository;

    public TripService(ITripRepository tripRepository, ITripIntervalRepository tripIntervalRepository,
        ITrainRepository trainRepository, IStationRepository stationRepository, IScheduleRepository scheduleRepository,
        IMapper mapper)
    {
        _tripRepository = tripRepository;
        _tripIntervalRepository = tripIntervalRepository;
        _trainRepository = trainRepository;
        _stationRepository = stationRepository;
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<TripDto>> GetById(int id)
    {
        var trip = await _tripRepository.GetById(id);

        if (trip is null)
        {
            var serviceResponse = new ServiceResponse<TripDto>
            {
                Success = false,
                Message = $"Trip with id: '{id} dose not exists"
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<TripDto>
        {
            Data = _mapper.Map<TripDto>(trip)
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<TripDto>>> GetAll()
    {
        var trips = await _tripRepository.GetAll();

        if (trips.Any() is false)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<TripDto>>
            {
                Success = false,
                Message = "Cannot find any trip"
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<IEnumerable<TripDto>>
        {
            Data = _mapper.Map<IEnumerable<TripDto>>(trips)
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<ConnectionTripDto>>> GetConnectionTrip(string startStation,
        string endStation, DateTime date)
    {
        var start = await _stationRepository.GetByName(startStation);

        if (start is null)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<ConnectionTripDto>>
            {
                Success = false,
                Message = $"Station with name: '{startStation}' does not exists."
            };

            return serviceResponse;
        }

        var end = await _stationRepository.GetByName(endStation);

        if (end is null)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<ConnectionTripDto>>
            {
                Success = false,
                Message = $"Station with name: '{endStation}' does not exists."
            };

            return serviceResponse;
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

        var response = new ServiceResponse<IEnumerable<ConnectionTripDto>>
        {
            Data = connectionTrips
        };

        return response;
    }

    public async Task<ServiceResponse<TripDto>> AddTrip(CreateTrip createTrip)
    {
        var train = await _trainRepository.GetTrainByName(createTrip.TrainName);

        if (train is null)
        {
            var serviceResponse = new ServiceResponse<TripDto>
            {
                Success = false,
                Message = $"Train with name: '{createTrip.TrainName}' does not exists."
            };

            return serviceResponse;
        }

        var tripInterval = new TripInterval
        {
            Monday = createTrip.TripIntervalDto.Monday,
            Tuesday = createTrip.TripIntervalDto.Tuesday,
            Wednesday = createTrip.TripIntervalDto.Wednesday,
            Thursday = createTrip.TripIntervalDto.Thursday,
            Friday = createTrip.TripIntervalDto.Friday,
            Saturday = createTrip.TripIntervalDto.Saturday,
            Sunday = createTrip.TripIntervalDto.Sunday
        };

        var trip = new Trip
        {
            Price = createTrip.Price,
            Train = train,
            TripInterval = tripInterval
        };

        var schedules = new List<Schedule>();
        foreach (var scheduleDto in createTrip.ScheduleDtos)
        {
            var station = await _stationRepository.GetByName(scheduleDto.StationName);
            if (station is null)
            {
                var serviceResponse = new ServiceResponse<TripDto>
                {
                    Success = false,
                    Message = $"Station with name: '{scheduleDto.StationName}' does not exists."
                };

                return serviceResponse;
            }

            if (scheduleDto.DepartureTime < scheduleDto.ArrivalTime)
            {
                var serviceResponse = new ServiceResponse<TripDto>
                {
                    Success = false,
                    Message = $"In station: '{scheduleDto.StationName}' departure time is before arrival time."
                };

                return serviceResponse;
            }

            schedules.Add(new Schedule
            {
                Trip = trip,
                Station = station,
                ArrivalTime = scheduleDto.ArrivalTime,
                DepartureTime = scheduleDto.DepartureTime,
                Platform = scheduleDto.Platform
            });
        }

        await _tripIntervalRepository.Add(tripInterval);
        await _tripRepository.Add(trip);
        await _scheduleRepository.AddRange(schedules);

        var response = new ServiceResponse<TripDto>
        {
            Data = _mapper.Map<TripDto>(trip)
        };

        return response;
    }

    public async Task<ServiceResponse<TripDto>> Delete(int id)
    {
        var trip = await _tripRepository.GetById(id);

        if (trip is null)
        {
            var serviceResponse = new ServiceResponse<TripDto>
            {
                Success = false,
                Message = $"Trip with id: '{id} dose not exists"
            };

            return serviceResponse;
        }

        var tripInterval = await _tripIntervalRepository.GetByTrip(trip);
        await _tripRepository.Remove(trip);
        await _tripIntervalRepository.Remove(tripInterval);

        var response = new ServiceResponse<TripDto>
        {
            Data = _mapper.Map<TripDto>(trip)
        };

        return response;
    }

    private async Task<IEnumerable<Trip>> GetAllTrips(Station station, DateTime tripDate)
    {
        var time = TimeOnly.FromDateTime(tripDate);
        var date = DateOnly.FromDateTime(tripDate);
        var schedules = await _scheduleRepository.GetByDepartureTimeAndStationId(time, station.Id);
        var trips = schedules.Select(x => x.Trip).Where(x => TripExtensions.IsTrainRunsOnGivenDate(x, date)).ToList();
        
        return trips;
    }
}