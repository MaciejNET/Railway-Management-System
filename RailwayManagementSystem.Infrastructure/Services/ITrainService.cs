using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public interface ITrainService
{
    Task<ServiceResponse<TrainDto>> GetById(int id);
    Task<ServiceResponse<TrainDto>> GetByTrainName(string name);
    Task<ServiceResponse<IEnumerable<TrainDto>>> GetByCareerId(int id);
    Task<ServiceResponse<IEnumerable<TrainDto>>> GetAll();
    Task<ServiceResponse<TrainDto>> AddTrain(TrainDto trainDto);
    Task<ServiceResponse<TrainDto>> Delete(int id);
}