using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface ICareerService
{
    Task<ServiceResponse<CareerDto>> GetById(int id);
    Task<ServiceResponse<CareerDto>> GetByName(string name);
    Task<ServiceResponse<IEnumerable<CareerDto>>> GetAll();
    Task<ServiceResponse<CareerDto>> AddCareer(CareerDto careerDto);
    Task<ServiceResponse<CareerDto>> Delete(int id);
}