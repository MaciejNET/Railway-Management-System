namespace RailwayManagementSystem.Application.DTO;

public class TripDto
{
    public Guid Id { get; set; }
    public string TrainName { get; set; }
    public string CarrierName { get; set; }
    public decimal Price { get; set; }
    public IEnumerable<ScheduleDto> Schedules { get; set; }
}