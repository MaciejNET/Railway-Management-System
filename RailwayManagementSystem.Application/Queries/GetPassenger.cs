using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Queries;

public class GetPassenger : IQuery<PassengerDto>
{
    public Guid Id { get; set; }
}