using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;

namespace RailwayManagementSystem.Application.Queries;

public sealed class GetTicket : IQuery<TicketDto>
{
    public Guid TicketId { get; set; }
}