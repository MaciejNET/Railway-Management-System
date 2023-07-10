using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.DTO;
using RailwayManagementSystem.Application.Responses;

namespace RailwayManagementSystem.Application.Queries;

public class GetConnections : IQuery<IEnumerable<Connection>>
{
    public string StartStation { get; set; }
    public string EndStation { get; set; }
    public DateTime DepartureTime { get; set; }
    public bool SearchIndirect { get; set; } = true;
}