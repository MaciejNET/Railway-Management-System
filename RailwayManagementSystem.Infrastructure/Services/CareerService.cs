using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public class CareerService : ICareerService
{
    private readonly ICareerRepository _careerRepository;
    private readonly IMapper _mapper;

    public CareerService(ICareerRepository careerRepository, IMapper mapper)
    {
        _careerRepository = careerRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<CareerDto>> GetById(int id)
    {
        var career = await _careerRepository.GetById(id);

        if (career is null)
        {
            var serviceResponse = new ServiceResponse<CareerDto>
            {
                Success = false,
                Message = "Career with id: '" + id + "' does not exists."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<CareerDto>
        {
            Data = _mapper.Map<CareerDto>(career)
        };

        return response;
    }

    public async Task<ServiceResponse<CareerDto>> GetByName(string name)
    {
        var career = await _careerRepository.GetByName(name);

        if (career is null)
        {
            var serviceResponse = new ServiceResponse<CareerDto>
            {
                Success = false,
                Message = "Career with name: '" + name + "' does not exists."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<CareerDto>
        {
            Data = _mapper.Map<CareerDto>(career)
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<CareerDto>>> GetAll()
    {
        var careers = await _careerRepository.GetAll();

        if (!careers.Any())
        {
            var serviceResponse = new ServiceResponse<IEnumerable<CareerDto>>
            {
                Success = false,
                Message = "Cannot find any career."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<IEnumerable<CareerDto>>
        {
            Data = _mapper.Map<IEnumerable<CareerDto>>(careers)
        };

        return response;
    }

    public async Task<ServiceResponse<CareerDto>> AddCareer(CareerDto careerDto)
    {
        var career = await _careerRepository.GetByName(careerDto.Name);
        if (career is not null)
        {
            var serviceResponse = new ServiceResponse<CareerDto>
            {
                Success = false,
                Message = "Career with name: '" + careerDto.Name + "' already exists"
            };

            return serviceResponse;
        }

        career = new Career
        {
            Name = careerDto.Name
        };
        await _careerRepository.Add(career);

        var response = new ServiceResponse<CareerDto>
        {
            Data = _mapper.Map<CareerDto>(career)
        };

        return response;
    }

    public async Task<ServiceResponse<CareerDto>> Delete(int id)
    {
        var career = await _careerRepository.GetById(id);

        if (career is null)
        {
            var serviceResponse = new ServiceResponse<CareerDto>
            {
                Success = false,
                Message = "Career with id: '" + id + "' does not exists."
            };

            return serviceResponse;
        }

        await _careerRepository.Remove(career);

        var response = new ServiceResponse<CareerDto>
        {
            Data = _mapper.Map<CareerDto>(career)
        };

        return response;
    }
}