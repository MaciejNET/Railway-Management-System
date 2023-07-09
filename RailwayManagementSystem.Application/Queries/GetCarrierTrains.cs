using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Queries;

public class GetCarrierTrains : IQuery<IEnumerable<TrainDto>>
{
    public Guid Id { get; set; }
}