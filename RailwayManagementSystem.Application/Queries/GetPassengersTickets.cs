using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Queries;

public class GetPassengersTickets : IQuery<IEnumerable<TicketDto>>
{
    public Guid PassengerId { get; set; }
}