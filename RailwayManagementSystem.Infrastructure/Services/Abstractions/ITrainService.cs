using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.Commands.Train;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services.Abstractions;

public interface ITrainService
{
    Task<ErrorOr<TrainDto>> GetById(int id);
    Task<ErrorOr<TrainDto>> GetByTrainName(string name);
    Task<ErrorOr<IEnumerable<TrainDto>>> GetByCarrierId(int id);
    Task<ErrorOr<IEnumerable<TrainDto>>> GetAll();
    Task<ErrorOr<TrainDto>> AddTrain(CreateTrain createTrain);
    Task<ErrorOr<Deleted>> Delete(int id);
}