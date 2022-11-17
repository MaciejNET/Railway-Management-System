using GemBox.Pdf;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface IPdfCreator
{
    byte[] CreateTicketPdf(Ticket ticket);
}