using ErrorOr;
using RailwayManagementSystem.Application.Commands.Trip;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface ITripService
{
    Task<TripDto> GetById(int id);
    Task<IEnumerable<TripDto>> GetAll();
    Task<IEnumerable<ConnectionTripDto>> GetConnectionTrip(string startStation, string endStation, DateTime date);
    Task<TripDto> AddTrip(CreateTrip createTrip);
    Task Delete(int id);
}