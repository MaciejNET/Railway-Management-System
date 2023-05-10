using ErrorOr;
using RailwayManagementSystem.Application.Commands.Train;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface ITrainService
{
    Task<TrainDto> GetById(int id);
    Task<TrainDto> GetByTrainName(string name);
    Task<IEnumerable<TrainDto>> GetByCarrierId(int id);
    Task<IEnumerable<TrainDto>> GetAll();
    Task<TrainDto> AddTrain(CreateTrain createTrain);
    Task Delete(int id);
}