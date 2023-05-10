using ErrorOr;
using RailwayManagementSystem.Application.Commands.Trip;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface ITripService
{
    Task<ErrorOr<TripDto>> GetById(int id);
    Task<ErrorOr<IEnumerable<TripDto>>> GetAll();

    Task<ErrorOr<IEnumerable<ConnectionTripDto>>> GetConnectionTrip(string startStation, string endStation,
        DateTime date);

    Task<ErrorOr<TripDto>> AddTrip(CreateTrip createTrip);
    Task<ErrorOr<Deleted>> Delete(int id);
}