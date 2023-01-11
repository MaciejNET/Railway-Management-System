using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IPdfCreator
{
    byte[] CreateTicketPdf(Ticket ticket);
}