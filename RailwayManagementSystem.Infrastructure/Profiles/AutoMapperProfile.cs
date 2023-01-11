using AutoMapper;
using RailwayManagementSystem.Core.Models;
using RailwayManagementSystem.Infrastructure.DTOs;

namespace RailwayManagementSystem.Infrastructure.Profiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Carrier, CarrierDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name.Value));
        
        CreateMap<Discount, DiscountDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name.Value));
        
        CreateMap<Passenger, PassengerDto>()
            .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName.Value))
            .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName.Value))
            .ForMember(dest => dest.Email, src => src.MapFrom(x => x.Email.Value))
            .ForMember(dest => dest.DiscountName, src => src.MapFrom(x => x.Discount != null ? x.Discount.Name.Value : null));
        
        CreateMap<Schedule, ScheduleDto>()
            .ForMember(dest => dest.StationName, src => src.MapFrom(x => x.Station.Name.Value));
        
        CreateMap<Station, StationDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name.Value))
            .ForMember(dest => dest.City, src => src.MapFrom(x => x.City.Value));
        
        CreateMap<Seat, SeatDto>();
        
        CreateMap<Ticket, TicketDto>()
            .ForMember(dest => dest.TripTrainName, src => src.MapFrom(x => x.Trip.Train.Name.Value))
            .ForMember(dest => dest.StartStation, src => src.MapFrom(x => x.Stations.First().Name.Value))
            .ForMember(dest => dest.EndStation, src => src.MapFrom(x => x.Stations.Last().Name.Value));
        
        CreateMap<Train, TrainDto>()
            .ForMember(dest => dest.CarrierName, src => src.MapFrom(x => x.Carrier.Name.Value))
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name.Value));
        
        CreateMap<TripInterval, TripIntervalDto>();
        
        CreateMap<Trip, TripDto>()
            .ForMember(dest => dest.TrainName, src => src.MapFrom(x => x.Train.Name.Value))
            .ForMember(dest => dest.StartStation, src => src.MapFrom(x => x.Schedules.First().Station.Name.Value))
            .ForMember(dest => dest.EndStation, src => src.MapFrom(x => x.Schedules.Last().Station.Name.Value))
            .ForMember(dest => dest.ArrivalTime, src => src.MapFrom(x => x.Schedules.Last().ArrivalTime))
            .ForMember(dest => dest.DepartureTime, src => src.MapFrom(x => x.Schedules.First().DepartureTime))
            .ForMember(dest => dest.TripIntervalDto, src => src.MapFrom(x => x.TripInterval));
        
        CreateMap<Admin, AdminDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name.Value));
        
        CreateMap<RailwayEmployee, RailwayEmployeeDto>()
            .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name.Value))
            .ForMember(dest => dest.FirstName, src => src.MapFrom(x => x.FirstName.Value))
            .ForMember(dest => dest.LastName, src => src.MapFrom(x => x.LastName.Value));
    }
}