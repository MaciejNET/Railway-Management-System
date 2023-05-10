using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Commands.Trip;

public class CreateTrip
{
    public decimal Price { get; set; }
    public string TrainName { get; set; } = string.Empty;
    public IEnumerable<ScheduleDto> ScheduleDtos { get; set; }
    public TripIntervalDto TripIntervalDto { get; set; }
}