using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Carrier;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface ICarrierService
{
    Task<ErrorOr<CarrierDto>> GetById(int id);
    Task<ErrorOr<CarrierDto>> GetByName(string name);
    Task<ErrorOr<IEnumerable<CarrierDto>>> GetAll();
    Task<ErrorOr<CarrierDto>> AddCarrier(CreateCarrier createCarrier);
    Task<ErrorOr<Deleted>> Delete(int id);
}