using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Commands.Trip;

public class CreateTrip
{
    public double Price { get; set; }
    public string TrainName { get; set; }
    public IEnumerable<ScheduleDto> ScheduleDtos { get; set; }
    public TripIntervalDto TripIntervalDto { get; set; }
}