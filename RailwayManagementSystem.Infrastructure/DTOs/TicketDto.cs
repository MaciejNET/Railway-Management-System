namespace RailwayManagementSystem.Infrastructure.DTOs;

public class TicketDto
{
    public int Id { get; set; }
    public string TripTrainName { get; set; }
    public double Price { get; set; }
    public SeatDto Seat { get; set; }
    public string StartStation { get; set; }
    public string EndStation { get; set; }
    public DateOnly TripDate { get; set; }
}