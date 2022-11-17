using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface ITicketService
{
    Task<ServiceResponse<TicketDto>> GetById(int id);
    Task<ServiceResponse<byte[]>> GetTicketPdf(int id);
    Task<ServiceResponse<string>> VerifyTicket(int id);
    Task<ServiceResponse<IEnumerable<TicketDto>>> GetByPassengerId(int id);
    Task Delete(int id);
}