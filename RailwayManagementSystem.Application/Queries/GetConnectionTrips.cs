using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Queries;

public class GetConnectionTrips : IQuery<IEnumerable<TripDto>>
{
    public string StartStation { get; set; }
    public string EndStation { get; set; }
    public DateTime DepartureTime { get; set; }
}