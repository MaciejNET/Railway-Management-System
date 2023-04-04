using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Passenger;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

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