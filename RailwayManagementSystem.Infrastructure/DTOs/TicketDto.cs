namespace RailwayManagementSystem.Infrastructure.DTOs;

public class TicketDto
{
    public int Id { get; set; }
    public string TripTrainName { get; set; } = string.Empty;
    public double Price { get; set; }
    public SeatDto Seat { get; set; }
    public string StartStation { get; set; } = string.Empty;
    public string EndStation { get; set; } = string.Empty;
    public DateOnly TripDate { get; set; }
}