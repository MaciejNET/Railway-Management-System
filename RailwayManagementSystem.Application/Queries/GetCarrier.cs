using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Queries;

public class GetCarrier : IQuery<CarrierDto>
{
    public Guid Id { get; set; }
}