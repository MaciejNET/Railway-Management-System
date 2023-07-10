using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Commands.Trip;

internal sealed class CreateTripHandler : ICommandHandler<CreateTrip>
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

        if (command.Schedule.Stations.Count < 2)
        {
            throw new TripStationsCountException();
        }
        
        var train = await _trainRepository.GetByNameAsync(trainName);

        if (train is null)
        {
            throw new TrainNotFoundException(trainName);
        }

        var validDate = new ValidDate(command.Schedule.ValidDate.From, command.Schedule.ValidDate.To);
        
        var tripAvailability = new TripAvailability(
            command.Schedule.TripAvailability.Monday,
            command.Schedule.TripAvailability.Tuesday,
            command.Schedule.TripAvailability.Wednesday,
            command.Schedule.TripAvailability.Thursday,
            command.Schedule.TripAvailability.Friday,
            command.Schedule.TripAvailability.Saturday,
            command.Schedule.TripAvailability.Sunday);
         
        var schedule = Schedule.Create(tripId, validDate, tripAvailability);
        
        var sortedStations = command.Schedule.Stations.OrderBy(x => x.DepartureTime).ToList();
        
        foreach (var scheduleStation in sortedStations)
        {
            var stationName = new StationName(scheduleStation.StationName);
            
            var station = await _stationRepository.GetByNameAsync(stationName);
                
            if (station is null)
            {
                throw new StationNotFoundException(stationName);
            }

            schedule.AddStationSchedule(StationSchedule.Create(station, scheduleStation.ArrivalTime, scheduleStation.DepartureTime, scheduleStation.Platform));
        }

         
        var trip = Core.Entities.Trip.Create(tripId, command.Price, train, schedule);
        
        train.AddTrip(trip);

        await _tripRepository.AddAsync(trip);
    }
}