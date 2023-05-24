using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Queries;

public class GetAvailableSeatsForTrip : IQuery<IEnumerable<SeatDto>>
{
    public Guid TripId { get; set; }
    public string StartStation { get; set; }
    public string EndStation { get; set; }
    public DateTime DepartureTime { get; set; }
}