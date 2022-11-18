using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Services;

public class CarrierService : ICarrierService
{
    private readonly ICarrierRepository _carrierRepository;
    private readonly IMapper _mapper;

    public CarrierService(ICarrierRepository carrierRepository, IMapper mapper)
    {
        _carrierRepository = carrierRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<CarrierDto>> GetById(int id)
    {
        var carrier = await _carrierRepository.GetById(id);

        if (carrier is null)
        {
            var serviceResponse = new ServiceResponse<CarrierDto>
            {
                Success = false,
                Message = "Carrier with id: '" + id + "' does not exists."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<CarrierDto>
        {
            Data = _mapper.Map<CarrierDto>(carrier)
        };

        return response;
    }

    public async Task<ServiceResponse<CarrierDto>> GetByName(string name)
    {
        var carrier = await _carrierRepository.GetByName(name);

        if (carrier is null)
        {
            var serviceResponse = new ServiceResponse<CarrierDto>
            {
                Success = false,
                Message = "Carrier with name: '" + name + "' does not exists."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<CarrierDto>
        {
            Data = _mapper.Map<CarrierDto>(carrier)
        };

        return response;
    }

    public async Task<ServiceResponse<IEnumerable<CarrierDto>>> GetAll()
    {
        var carriers = await _carrierRepository.GetAll();

        if (!carriers.Any())
        {
            var serviceResponse = new ServiceResponse<IEnumerable<CarrierDto>>
            {
                Success = false,
                Message = "Cannot find any carrier."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<IEnumerable<CarrierDto>>
        {
            Data = _mapper.Map<IEnumerable<CarrierDto>>(carriers)
        };

        return response;
    }

    public async Task<ServiceResponse<CarrierDto>> AddCarrier(CarrierDto careerDto)
    {
        var carrier = await _carrierRepository.GetByName(careerDto.Name);
        if (carrier is not null)
        {
            var serviceResponse = new ServiceResponse<CarrierDto>
            {
                Success = false,
                Message = "Carrier with name: '" + careerDto.Name + "' already exists"
            };

            return serviceResponse;
        }

        carrier = new Carrier
        {
            Name = careerDto.Name
        };
        await _carrierRepository.Add(carrier);

        var response = new ServiceResponse<CarrierDto>
        {
            Data = _mapper.Map<CarrierDto>(carrier)
        };

        return response;
    }

    public async Task<ServiceResponse<CarrierDto>> Delete(int id)
    {
        var career = await _carrierRepository.GetById(id);

        if (career is null)
        {
            var serviceResponse = new ServiceResponse<CarrierDto>
            {
                Success = false,
                Message = "Carrier with id: '" + id + "' does not exists."
            };

            return serviceResponse;
        }

        await _carrierRepository.Remove(career);

        var response = new ServiceResponse<CarrierDto>
        {
            Data = _mapper.Map<CarrierDto>(career)
        };

        return response;
    }
}