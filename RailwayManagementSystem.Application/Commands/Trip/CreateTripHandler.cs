using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
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
         var train = await _trainRepository.GetByNameAsync(command.TrainName);

         if (train is null)
         {
             throw new TrainNotFoundException(command.TrainName);
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
             var station = await _stationRepository.GetByNameAsync(schedule.StationName);
                 
             if (station is null)
             {
                 throw new StationNotFoundException(schedule.StationName);
             }

             if (schedule.DepartureTime < schedule.ArrivalTime)
             {
                 throw new InvalidDepartureTimeException(schedule.StationName);
             }

             schedules.Add(
                 Schedule.Create(
                     command.Id,
                     station,
                     schedule.ArrivalTime,
                     schedule.DepartureTime,
                     schedule.Platform));
         }
         
         var trip = Core.Entities.Trip.Create(command.Id, command.Price, train, tripInterval, schedules);

         await _tripRepository.AddAsync(trip);
    }
}