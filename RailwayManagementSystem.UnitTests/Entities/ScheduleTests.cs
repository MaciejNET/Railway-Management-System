using FluentAssertions;
using Microsoft.Extensions.Time.Testing;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Entities;

public class ScheduleTests
{
    [Theory]
    [InlineData("2023-07-10")]
    [InlineData("2023-07-11")]
    [InlineData("2023-07-12")]
    [InlineData("2023-08-10")]
    public void IsTripRunningOnGivenDate_GivenValidDate_ShouldReturnTrue(DateTime date)
    {
        //Arrange

        //Act
        var result = _schedule.IsTripRunningOnGivenDate(date);
        
        //Assert
        result.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("2023-06-13")]
    [InlineData("2023-07-15")]
    [InlineData("2023-10-19")]
    public void IsTripRunningOnGivenDate_GivenInvalidDate_ShouldReturnFalse(DateTime date)
    {
        //Arrange

        //Act
        var result = _schedule.IsTripRunningOnGivenDate(date);
        
        //Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void ValidDate_CreateValidDate_ShouldReturnValidDate()
    {
        //Arrange
        var from = DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime);
        var to = DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime.AddMonths(3));
        
        //Act
        var result = new ValidDate(from, to);
        
        //Assert
        result.From.Should().Be(from);
        result.To.Should().Be(to);
    }
    
    [Fact]
    public void ValidDate_CreateInvalidDate_ShouldThrowInvalidDateRangeException()
    {
        //Arrange
        var from = DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime.AddMonths(3));
        var to = DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime);
        
        //Act
        var exception = Record.Exception(() => new ValidDate(from, to));

        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidDateRangeException>();
    }
    
    #region ARRANGE

    private readonly FakeTimeProvider _timeProvider;
    private readonly Station _station1;
    private readonly Station _station2;
    private readonly Station _station3;
    private readonly List<StationSchedule> _stationSchedules;
    private readonly Schedule _schedule;

    public ScheduleTests()
    {
        _timeProvider = new FakeTimeProvider();
        _timeProvider.SetUtcNow(new DateTime(2023, 7, 10, 12, 0, 0));
        
        _station1 = Station.Create(
            id: Guid.NewGuid(),
            name: "Starachowice",
            city: "Starachowice",
            numberOfPlatforms: 2);

        _station2 = Station.Create(
            id: Guid.NewGuid(),
            name: "Kielce",
            city: "Kielce",
            numberOfPlatforms: 6);

        _station3 = Station.Create(
            id: Guid.NewGuid(),
            name: "Krakow Glowny",
            city: "Krakow",
            numberOfPlatforms: 12);
        
        _stationSchedules = 
        [
            StationSchedule.Create(_station1, new TimeOnly(9, 00), new TimeOnly(9, 00), 2),
            StationSchedule.Create(_station2, new TimeOnly(10, 00), new TimeOnly(10, 05), 1),
            StationSchedule.Create(_station3, new TimeOnly(12, 15), new TimeOnly(12, 15), 1),
        ];
        
        var tripStationSchedules = _stationSchedules.OrderBy(x => x.DepartureTime);
        
        _schedule = Schedule.Create(
            tripId: TripId.Create(),
            validDate: new ValidDate(DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime), DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime.AddMonths(3))),
            tripAvailability: new TripAvailability(
                Monday: true,
                Tuesday: true,
                Wednesday: true,
                Thursday: true,
                Friday: true,
                Saturday: false,
                Sunday: false));

        foreach (var schedule in tripStationSchedules)
        {
            _schedule.AddStationSchedule(schedule);
        }
    }
    
    #endregion
}