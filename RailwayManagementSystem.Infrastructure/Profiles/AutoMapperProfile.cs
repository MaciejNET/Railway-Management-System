using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Career, CareerDto>();
        CreateMap<Discount, DiscountDto>();
        CreateMap<Passenger, PassengerDto>()
            .ForMember(dest => dest.DiscountName, src => src.MapFrom(x => x.Discount.Name));
        CreateMap<Schedule, ScheduleDto>()
            .ForMember(dest => dest.StationName, src => src.MapFrom(x => x.Station.Name));
        CreateMap<Station, StationDto>();
        CreateMap<Seat, SeatDto>();
        CreateMap<Ticket, TicketDto>()
            .ForMember(dest => dest.TripTrainName, src => src.MapFrom(x => x.Trip.Train.Name))
            .ForMember(dest => dest.StartStation, src => src.MapFrom(x => x.Stations.First().Name))
            .ForMember(dest => dest.EndStation, src => src.MapFrom(x => x.Stations.Last().Name));
        CreateMap<Train, TrainDto>()
            .ForMember(dest => dest.CareerName, src => src.MapFrom(x => x.Career.Name));
        CreateMap<TripInterval, TripIntervalDto>();
        CreateMap<Trip, TripDto>()
            .ForMember(dest => dest.TrainName, src => src.MapFrom(x => x.Train.Name))
            .ForMember(dest => dest.StartStation, src => src.MapFrom(x => x.Schedules.First().Station.Name))
            .ForMember(dest => dest.EndStation, src => src.MapFrom(x => x.Schedules.Last().Station.Name))
            .ForMember(dest => dest.ArrivalTime, src => src.MapFrom(x => x.Schedules.Last().ArrivalTime))
            .ForMember(dest => dest.DepartureTime, src => src.MapFrom(x => x.Schedules.First().DepartureTime))
            .ForMember(dest => dest.TripIntervalDto, src => src.MapFrom(x => x.TripInterval));
    }
}