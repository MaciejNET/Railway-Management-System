using AutoMapper;
using ErrorOr;
using RailwayManagementSystem.Application.DTOs;
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

    public async Task<ErrorOr<IEnumerable<ScheduleDto>>> GetByTripId(int id)
    {
        if (await _tripRepository.GetByIdAsync(id) is null)
        {
            return Error.NotFound($"Trip with id: '{id}' does not exists.");
        }
        
        var schedules = await _scheduleRepository.GetByTripIdAsync(id);

        if (!schedules.Any())
        {
            return Error.NotFound($"Cannot find any schedule for trip with id : '{id}'.");
        }

        var schedulesDto = _mapper.Map<IEnumerable<ScheduleDto>>(schedules);

        return schedulesDto.ToList();
    }
}