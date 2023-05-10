using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.DTOs;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Application.Services.Abstractions;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.Application.Services.Implementations;

public class ScheduleService : IScheduleService
{
    private readonly IMapper _mapper;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly ITripRepository _tripRepository;

    public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, ITripRepository tripRepository)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
        _tripRepository = tripRepository;
    }

    public async Task<IEnumerable<ScheduleDto>> GetByTripId(int id)
    {
        if (await _tripRepository.GetByIdAsync(id) is null)
        {
            throw new TripNotFoundException(id);
        }
        
        var schedules = await _scheduleRepository.GetByTripIdAsync(id);

        if (!schedules.Any())
        {
            return new List<ScheduleDto>();
        }

        var schedulesDto = _mapper.Map<IEnumerable<ScheduleDto>>(schedules);

        return schedulesDto.ToList();
    }
}