using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.Core.Entities;

public sealed class StationSchedule
{
    public StationScheduleId Id { get; private set; }
    public Station Station { get; private set; }
    public TimeOnly ArrivalTime { get; private set; }
    public TimeOnly DepartureTime { get; private set; }
    public int Platform { get; private set; }

    private StationSchedule(Station station, TimeOnly arrivalTime, TimeOnly departureTime, int platform)
    {
        Id = StationScheduleId.Create();
        Station = station;
        ArrivalTime = arrivalTime;
        DepartureTime = departureTime;
        Platform = platform;
    }

    public static StationSchedule Create(Station station, TimeOnly arrivalTime, TimeOnly departureTime, int platform)
    {
        if (departureTime < arrivalTime)
        {
            throw new InvalidDepartureTimeException(station.Name);
        }

        return new StationSchedule(station, arrivalTime, departureTime, platform);
    }
    
    private StationSchedule() {}
}