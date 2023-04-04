using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface ITicketService
{
    Task<ErrorOr<TicketDto>> GetById(int id);
    Task<ErrorOr<byte[]>> GetTicketPdf(int id);
    Task<ErrorOr<VerifyTicketResponse>> VerifyTicket(int id);
    Task<ErrorOr<IEnumerable<TicketDto>>> GetByPassengerId(int id);
    Task<ErrorOr<Success>> Cancel(int id);
}