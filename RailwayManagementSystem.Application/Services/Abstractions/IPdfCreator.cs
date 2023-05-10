using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IPdfCreator
{
    byte[] CreateTicketPdf(Ticket ticket);
}