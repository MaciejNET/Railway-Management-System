using RailwayManagementSystem.Core.Enums;

namespace RailwayManagementSystem.Application.DTO;

public class TicketDto
{
    public Guid Id { get; set; }
    public string TrainName { get; set; }
    public string CarrierName { get; set; }
    public decimal Price { get; set; }
    public string StartStation { get; set; }
    public string EndStation { get; set; }
    public DateTime TripDate { get; set; }
    public int SeatNumber { get; set; }
    public Place Place { get; set; }
}