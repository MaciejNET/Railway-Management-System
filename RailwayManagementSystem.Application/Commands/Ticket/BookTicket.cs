using System.Text.Json.Serialization;
using DateOnlyTimeOnly.AspNet.Converters;
using RailwayManagementSystem.Application.Abstractions;

namespace RailwayManagementSystem.Application.Commands.Ticket;

public record BookTicket(Guid TripId, Guid PassengerId, DateTime TripDate, string StartStation, string EndStation, Guid? SeatId = null) : ICommand;