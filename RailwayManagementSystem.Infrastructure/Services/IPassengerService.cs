using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Passenger;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface IPassengerService
{
    Task<ServiceResponse<PassengerDto>> GetById(int id);
    Task<ServiceResponse<IEnumerable<PassengerDto>>> GetAll();
    Task<ServiceResponse<string>> Login(LoginPassenger loginPassenger);
    Task<ServiceResponse<PassengerDto>> Register(RegisterPassenger registerPassenger);
    Task<ServiceResponse<PassengerDto>> Update(int id, UpdatePassenger updatePassenger);
    Task<ServiceResponse<PassengerDto>> UpdateDiscount(int id, string? discountName);
    Task<ServiceResponse<PassengerDto>> Delete(int id);
}