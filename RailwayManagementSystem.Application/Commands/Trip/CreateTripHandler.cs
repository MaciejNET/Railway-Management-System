using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Trip;

public class CreateTripHandler : ICommandHandler<CreateTrip>
{
    private readonly IStationRepository _stationRepository;
    private readonly ITrainRepository _trainRepository;
    private readonly ITripRepository _tripRepository;
    
    public CreateTripHandler(ITripRepository tripRepository, IStationRepository stationRepository, ITrainRepository trainRepository)
    {
        _tripRepository = tripRepository;
        _stationRepository = stationRepository;
        _trainRepository = trainRepository;
    }

    public async Task HandleAsync(CreateTrip command)
    {
        var tripId = new TripId(command.Id);
        var trainName = new TrainName(command.TrainName);

        if (command.Schedules.Count() < 2)
        {
            throw new TripSchedulesCountException();
        }
        
        var train = await _trainRepository.GetByNameAsync(trainName);

        if (train is null)
        {
            throw new TrainNotFoundException(trainName);
        }

        var tripInterval = new TripInterval(
            command.TripInterval.Monday,
            command.TripInterval.Tuesday,
            command.TripInterval.Wednesday,
            command.TripInterval.Thursday,
            command.TripInterval.Friday,
            command.TripInterval.Saturday,
            command.TripInterval.Sunday);
         
        List<Schedule> schedules = new();
        foreach (var schedule in command.Schedules)
        {
            var stationName = new StationName(schedule.StationName);
            
            var station = await _stationRepository.GetByNameAsync(stationName);
                
            if (station is null)
            {
                throw new StationNotFoundException(stationName);
            }

            schedules.Add(
                Schedule.Create(
                    tripId,
                    station,
                    schedule.ArrivalTime,
                    schedule.DepartureTime,
                    schedule.Platform));
        }
         
        var trip = Core.Entities.Trip.Create(command.Id, command.Price, train, tripInterval, schedules);

        await _tripRepository.AddAsync(trip);
    }
}