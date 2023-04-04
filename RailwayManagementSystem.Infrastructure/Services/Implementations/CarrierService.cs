using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;
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

    public async Task<ErrorOr<CarrierDto>> GetById(int id)
    {
        var carrier = await _carrierRepository.GetByIdAsync(id);

        if (carrier is null)
        {
            return Error.NotFound($"Carrier with id: '{id}' does not exists.");
        }

        return _mapper.Map<CarrierDto>(carrier);
    }

    public async Task<ErrorOr<CarrierDto>> GetByName(string name)
    {
        var carrier = await _carrierRepository.GetByNameAsync(name);

        if (carrier is null)
        {
            return Error.NotFound($"Carrier with name: '{name}' does not exists.");
        }

        return _mapper.Map<CarrierDto>(carrier);
    }

    public async Task<ErrorOr<IEnumerable<CarrierDto>>> GetAll()
    {
        var carriers = await _carrierRepository.GetAllAsync();

        if (!carriers.Any())
        {
            return Error.NotFound("Cannot find any carrier.");
        }

        var carriersDto = _mapper.Map<IEnumerable<CarrierDto>>(carriers);
        
        return carriersDto.ToList();
    }

    public async Task<ErrorOr<CarrierDto>> AddCarrier(CreateCarrier createCarrier)
    {
        var carrier = await _carrierRepository.GetByNameAsync(createCarrier.Name);
        
        if (carrier is not null)
        {
            return Error.Validation($"Carrier with name: '{createCarrier.Name}' already exists");
        }
        
        ErrorOr<CarrierName> name = CarrierName.Create(createCarrier.Name);

        if (name.IsError)
        { 
            return name.Errors;
        }

        carrier = Carrier.Create(name.Value);

        await _carrierRepository.AddAsync(carrier);

        return _mapper.Map<CarrierDto>(carrier);
    }

    public async Task<ErrorOr<Deleted>> Delete(int id)
    {
        var career = await _carrierRepository.GetByIdAsync(id);

        if (career is null)
        {
            return Error.NotFound($"Carrier with id: '{id}' does not exists.");
        }

        await _carrierRepository.RemoveAsync(career);

        return Result.Deleted;
    }
}