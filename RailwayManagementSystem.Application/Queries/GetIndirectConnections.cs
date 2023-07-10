using RailwayManagementSystem.Application.Abstractions;
using RailwayManagementSystem.Application.Responses;

namespace RailwayManagementSystem.Application.Queries;

public class GetIndirectConnections : IQuery<IEnumerable<Connection>>
{
    public string StartStation { get; set; }
    public string EndStation { get; set; }
    public DateTime DepartureTime { get; set; }
}