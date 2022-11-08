using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface ITicketService
{
    Task<ServiceResponse<IEnumerable<TicketDto>>> GetByPassengerId(int id);
    Task Delete(int id);
}