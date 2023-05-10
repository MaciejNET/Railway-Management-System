using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.Commands.Carrier;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Application.Services.Implementations;

public class CarrierService : ICarrierService
{
    private readonly ICarrierRepository _carrierRepository;
    private readonly IMapper _mapper;

    public CarrierService(ICarrierRepository carrierRepository, IMapper mapper)
    {
        _carrierRepository = carrierRepository;
        _mapper = mapper;
    }

    public async Task<CarrierDto> GetById(int id)
    {
        var carrier = await _carrierRepository.GetByIdAsync(id);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(id);
        }

        return _mapper.Map<CarrierDto>(carrier);
    }

    public async Task<CarrierDto> GetByName(string name)
    {
        var carrier = await _carrierRepository.GetByNameAsync(name);

        if (carrier is null)
        {
            throw new CarrierNotFoundException(name);
        }

        return _mapper.Map<CarrierDto>(carrier);
    }

    public async Task<IEnumerable<CarrierDto>> GetAll()
    {
        var carriers = await _carrierRepository.GetAllAsync();

        if (!carriers.Any())
        {
            return new List<CarrierDto>();
        }

        var carriersDto = _mapper.Map<IEnumerable<CarrierDto>>(carriers);
        
        return carriersDto.ToList();
    }

    public async Task<CarrierDto> AddCarrier(CreateCarrier createCarrier)
    {
        var carrier = await _carrierRepository.GetByNameAsync(createCarrier.Name);
        
        if (carrier is not null)
        {
            throw new CarrierNotFoundException(createCarrier.Name);
        }
        
        var name = new CarrierName(createCarrier.Name);

        carrier = Carrier.Create(name);

        await _carrierRepository.AddAsync(carrier);

        return _mapper.Map<CarrierDto>(carrier);
    }

    public async Task Delete(int id)
    {
        var career = await _carrierRepository.GetByIdAsync(id);

        if (career is null)
        {
            throw new CarrierNotFoundException(id);
        }

        await _carrierRepository.RemoveAsync(career);
    }
}