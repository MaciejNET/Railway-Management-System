using QRCoder;
using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Services;

public class QrCreator : IQrCreator
{
    public byte[] CreateTicketQrCode(Ticket ticket)
    {
        var qrGenerator = new QRCodeGenerator();
        var qrCodeData = qrGenerator.CreateQrCode($"https://localhost:7194/api/tickets/{ticket.Id}/verify", QRCodeGenerator.ECCLevel.Q);
        var qrCode = new BitmapByteQRCode(qrCodeData);
        var qrCodeImage = qrCode.GetGraphic(3);

        return qrCodeImage;
    }
}