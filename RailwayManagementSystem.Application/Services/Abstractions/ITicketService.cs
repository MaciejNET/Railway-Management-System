using ErrorOr;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface ITicketService
{
    Task<TicketDto> GetById(int id);
    Task<byte[]> GetTicketPdf(int id);
    Task<VerifyTicketResponse> VerifyTicket(int id);
    Task<IEnumerable<TicketDto>> GetByPassengerId(int id);
    Task Cancel(int id);
}