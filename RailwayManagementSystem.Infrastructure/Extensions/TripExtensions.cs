using RailwayManagementSystem.Core.Models;

namespace RailwayManagementSystem.Infrastructure.Extensions;

public static class TripExtensions
{
    public static bool IsTrainRunsOnGivenDate(Trip trip, DateOnly tripDate)
    {
        return tripDate.ToString("dddd") switch
        {
            "Monday" => trip.TripInterval.Monday,
            "Tuesday" => trip.TripInterval.Tuesday,
            "Wednesday" => trip.TripInterval.Wednesday,
            "Thursday" => trip.TripInterval.Thursday,
            "Friday" => trip.TripInterval.Friday,
            "Saturday" => trip.TripInterval.Saturday,
            "Sunday" => trip.TripInterval.Sunday,
            _ => false
        };
    }
}