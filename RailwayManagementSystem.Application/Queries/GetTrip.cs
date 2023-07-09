using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Queries;

public class GetTrip : IQuery<TripDto>
{
    public Guid Id { get; set; }
}