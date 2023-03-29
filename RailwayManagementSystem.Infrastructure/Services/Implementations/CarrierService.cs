using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.Commands.Carrier;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

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
        var carrier = await _carrierRepository.GetByIdAsync(id);

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
        var carrier = await _carrierRepository.GetByNameAsync(name);

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
        var carriers = await _carrierRepository.GetAllAsync();

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

    public async Task<ServiceResponse<CarrierDto>> AddCarrier(CreateCarrier createCarrier)
    {
        var carrier = await _carrierRepository.GetByNameAsync(createCarrier.Name);
        if (carrier is not null)
        {
            var serviceResponse = new ServiceResponse<CarrierDto>
            {
                Success = false,
                Message = "Carrier with name: '" + createCarrier.Name + "' already exists"
            };

            return serviceResponse;
        }

        try
        {
            carrier = new Carrier
            {
                Name = createCarrier.Name
            };
        
            await _carrierRepository.AddAsync(carrier);
            await _carrierRepository.SaveChangesAsync();

            var response = new ServiceResponse<CarrierDto>
            {
                Data = _mapper.Map<CarrierDto>(carrier)
            };

            return response;
        }
        catch (Exception e)
        {
            var response = new ServiceResponse<CarrierDto>
            {
                Success = false,
                Message = e.Message
            };

            return response;
        }
    }

    public async Task<ServiceResponse<CarrierDto>> Delete(int id)
    {
        var career = await _carrierRepository.GetByIdAsync(id);

        if (career is null)
        {
            var serviceResponse = new ServiceResponse<CarrierDto>
            {
                Success = false,
                Message = "Carrier with id: '" + id + "' does not exists."
            };

            return serviceResponse;
        }

        await _carrierRepository.RemoveAsync(career);
        await _carrierRepository.SaveChangesAsync();

        var response = new ServiceResponse<CarrierDto>
        {
            Data = _mapper.Map<CarrierDto>(career)
        };

        return response;
    }
}