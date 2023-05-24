using RailwayManagementSystem.Core.Enums;

namespace RailwayManagementSystem.Application.DTO;

public class SeatDto
{
    public Guid Id { get; set; }
    public int SeatNumber { get; set; }
    public Place Place { get; set; }
}