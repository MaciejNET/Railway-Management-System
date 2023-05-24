using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Trip;

public record CreateTrip(Guid Id, decimal Price, string TrainName, IEnumerable<ScheduleWriteModel> Schedules, TripIntervalWriteModel TripInterval) : ICommand;

public record ScheduleWriteModel(string StationName, DateTime DepartureTime, DateTime ArrivalTime, int Platform);

public record TripIntervalWriteModel(
    bool Monday,
    bool Tuesday,
    bool Wednesday,
    bool Thursday,
    bool Friday,
    bool Saturday,
    bool Sunday);