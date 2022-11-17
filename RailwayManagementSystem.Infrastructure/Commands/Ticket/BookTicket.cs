using System.Text.Json.Serialization;
using DateOnlyTimeOnly.AspNet.Converters;

namespace RailwayManagementSystem.Infrastructure.Commands.Ticket;

public class BookTicket
{
    public int TripId { get; set; }
    public int PassengerId { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly TripDate { get; set; }

    public string StartStation { get; set; } = string.Empty;
    public string EndStation { get; set; } = string.Empty;
}