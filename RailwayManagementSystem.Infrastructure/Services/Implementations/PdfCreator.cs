using GemBox.Pdf;
using GemBox.Pdf.Content;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

public class PdfCreator : IPdfCreator
{
    private readonly IQrCreator _qrCreator;

    public PdfCreator(IQrCreator qrCreator)
    {
        _qrCreator = qrCreator;
    }

    public byte[] CreateTicketPdf(Ticket ticket)
    {
        ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        var qrCode = _qrCreator.CreateTicketQrCode(ticket);
        using var document = new PdfDocument();
        var qr = PdfImage.Load(new MemoryStream(qrCode));
        var page = document.Pages.Add();
        
        using var formattedText = new PdfFormattedText();
        formattedText.AppendLine($"Train: {ticket.Trip.Train.Name.Value}");
        formattedText.AppendLine($"From: {ticket.Stations.First().Name.Value} - To: {ticket.Stations.Last().Name.Value}");
        formattedText.AppendLine($"Trip date: {ticket.TripDate}");
        formattedText.AppendLine($"Seat: {ticket.Seat.SeatNumber} - {ticket.Seat.Place}");
        
        page.Content.DrawText(formattedText, new PdfPoint(75, 750));
        page.Content.DrawImage(qr, new PdfPoint(75, 630));
        var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }
}