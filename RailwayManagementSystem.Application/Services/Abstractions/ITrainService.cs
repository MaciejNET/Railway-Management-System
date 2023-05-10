using ErrorOr;
using RailwayManagementSystem.Application.Commands.Train;
using RailwayManagementSystem.Application.DTOs;

namespace RailwayManagementSystem.Application.Services.Abstractions;

public interface ITrainService
{
    Task<ErrorOr<TrainDto>> GetById(int id);
    Task<ErrorOr<TrainDto>> GetByTrainName(string name);
    Task<ErrorOr<IEnumerable<TrainDto>>> GetByCarrierId(int id);
    Task<ErrorOr<IEnumerable<TrainDto>>> GetAll();
    Task<ErrorOr<TrainDto>> AddTrain(CreateTrain createTrain);
    Task<ErrorOr<Deleted>> Delete(int id);
}