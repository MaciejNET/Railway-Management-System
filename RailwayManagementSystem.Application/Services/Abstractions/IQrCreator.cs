using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IQrCreator
{
    byte[] CreateTicketQrCode(Ticket ticket);
}