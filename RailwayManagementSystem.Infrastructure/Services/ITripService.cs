using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Trip;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface ITripService
{
    Task<ServiceResponse<TripDto>> GetById(int id);
    Task<ServiceResponse<IEnumerable<TripDto>>> GetAll();

    Task<ServiceResponse<IEnumerable<ConnectionTripDto>>> GetConnectionTrip(string startStation, string endStation,
        DateTime date);

    Task<ServiceResponse<TripDto>> AddTrip(CreateTrip createTrip);
    Task<ServiceResponse<TripDto>> Delete(int id);
}