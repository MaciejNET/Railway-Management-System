using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface IQrCreator
{
    byte[] CreateTicketQrCode(Ticket ticket);
}