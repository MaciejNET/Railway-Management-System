using ErrorOr;
using RailwayManagementSystem.Application.Commands.Carrier;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface ICarrierService
{
    Task<CarrierDto> GetById(int id);
    Task<CarrierDto> GetByName(string name);
    Task<IEnumerable<CarrierDto>> GetAll();
    Task<CarrierDto> AddCarrier(CreateCarrier createCarrier);
    Task Delete(int id);
}