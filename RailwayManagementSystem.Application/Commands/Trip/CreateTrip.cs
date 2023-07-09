using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Trip;

public record CreateTrip(Guid Id, decimal Price, string TrainName, ScheduleWriteModel Schedule) : ICommand;

public record ScheduleWriteModel(ValidDateWriteModel ValidDate, TripAvailabilityWriteModel TripAvailability, List<StationScheduleWriteModel> Stations);

public record ValidDateWriteModel(DateOnly From, DateOnly To);

public record TripAvailabilityWriteModel(
    bool Monday,
    bool Tuesday,
    bool Wednesday,
    bool Thursday,
    bool Friday,
    bool Saturday,
    bool Sunday);

public record StationScheduleWriteModel(string StationName, TimeOnly ArrivalTime, TimeOnly DepartureTime, int Platform); 