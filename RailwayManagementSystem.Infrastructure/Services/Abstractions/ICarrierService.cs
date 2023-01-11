using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface ICarrierService
{
    Task<ServiceResponse<CarrierDto>> GetById(int id);
    Task<ServiceResponse<CarrierDto>> GetByName(string name);
    Task<ServiceResponse<IEnumerable<CarrierDto>>> GetAll();
    Task<ServiceResponse<CarrierDto>> AddCarrier(string name);
    Task<ServiceResponse<CarrierDto>> Delete(int id);
}