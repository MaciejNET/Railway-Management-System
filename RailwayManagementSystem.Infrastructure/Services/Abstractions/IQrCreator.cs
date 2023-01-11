using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface IQrCreator
{
    byte[] CreateTicketQrCode(Ticket ticket);
}