using ErrorOr;
using RailwayManagementSystem.Application.Commands.Passenger;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface IPassengerService
{
    Task<ErrorOr<PassengerDto>> GetById(int id);
    Task<ErrorOr<IEnumerable<PassengerDto>>> GetAll();
    Task<ErrorOr<string>> Login(LoginPassenger loginPassenger);
    Task<ErrorOr<PassengerDto>> Register(RegisterPassenger registerPassenger);
    Task<ErrorOr<Updated>> Update(int id, UpdatePassenger updatePassenger);
    Task<ErrorOr<Updated>> UpdateDiscount(int id, string? discountName);
    Task<ErrorOr<Deleted>> Delete(int id);
}