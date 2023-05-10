using ErrorOr;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IPassengerService
{
    Task<PassengerDto> GetById(int id);
    Task<IEnumerable<PassengerDto>> GetAll();
    Task<string> Login(LoginPassenger loginPassenger);
    Task<PassengerDto> Register(RegisterPassenger registerPassenger);
    Task<Updated> Update(int id, UpdatePassenger updatePassenger);
    Task UpdateDiscount(int id, string? discountName);
    Task Delete(int id);
}