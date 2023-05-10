using RailwayManagementSystem.Core.Models.Enums;

namespace RailwayManagementSystem.Application.DTOs;

public class SeatDto
{
    public int Id { get; set; }
    public int SeatNumber { get; set; }
    public Place Place { get; set; }
}