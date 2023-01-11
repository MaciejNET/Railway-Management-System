using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Infrastructure.DTOs;
using RailwayManagementSystem.Infrastructure.Services.Abstractions;

namespace RailwayManagementSystem.Infrastructure.Services.Implementations;

public class ScheduleService : IScheduleService
{
    private readonly IMapper _mapper;
    private readonly IScheduleRepository _scheduleRepository;

    public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<IEnumerable<ScheduleDto>>> GetByTripId(int id)
    {
        var schedules = await _scheduleRepository.GetByTripId(id);

        if (schedules.Any() is false)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<ScheduleDto>>
            {
                Success = false,
                Message = $"Cannot find any schedule for trip with id : '{id}'."
            };

            return serviceResponse;
        }

        var response = new ServiceResponse<IEnumerable<ScheduleDto>>
        {
            Data = _mapper.Map<IEnumerable<ScheduleDto>>(schedules)
        };

        return response;
    }
}