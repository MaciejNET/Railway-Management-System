using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Train;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface ITrainService
{
    Task<ServiceResponse<TrainDto>> GetById(int id);
    Task<ServiceResponse<TrainDto>> GetByTrainName(string name);
    Task<ServiceResponse<IEnumerable<TrainDto>>> GetByCarrierId(int id);
    Task<ServiceResponse<IEnumerable<TrainDto>>> GetAll();
    Task<ServiceResponse<TrainDto>> AddTrain(CreateTrain createTrain);
    Task<ServiceResponse<TrainDto>> Delete(int id);
}