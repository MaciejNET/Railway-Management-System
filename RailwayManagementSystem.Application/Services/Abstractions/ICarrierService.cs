using ErrorOr;
using RailwayManagementSystem.Application.Commands.Carrier;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface ICarrierService
{
    Task<ErrorOr<CarrierDto>> GetById(int id);
    Task<ErrorOr<CarrierDto>> GetByName(string name);
    Task<ErrorOr<IEnumerable<CarrierDto>>> GetAll();
    Task<ErrorOr<CarrierDto>> AddCarrier(CreateCarrier createCarrier);
    Task<ErrorOr<Deleted>> Delete(int id);
}