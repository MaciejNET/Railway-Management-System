using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Trip;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface ITripService
{
    Task<ErrorOr<TripDto>> GetById(int id);
    Task<ErrorOr<IEnumerable<TripDto>>> GetAll();

    Task<ErrorOr<IEnumerable<ConnectionTripDto>>> GetConnectionTrip(string startStation, string endStation,
        DateTime date);

    Task<ErrorOr<TripDto>> AddTrip(CreateTrip createTrip);
    Task<ErrorOr<Deleted>> Delete(int id);
}