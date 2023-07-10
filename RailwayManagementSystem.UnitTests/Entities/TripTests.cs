using FluentAssertions;
using Humanizer;
using RailwayManagementSystem.Core.Abstractions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Exceptions;
using RailwayManagementSystem.Core.ValueObjects;
using RailwayManagementSystem.UnitTests.Shared;

namespace RailwayManagementSystem.UnitTests.Entities;

public class TripTests
{
    [Fact]
    public void ReserveTicket_GivenValidStationsAndValidTripDate_ShouldSucceed()
    {
        //Arrange
        var trip = InitTrip();
        var passenger = InitPassenger();
        
        //Act
        trip.ReserveTicket(passenger, _station1, _station3, _clock.Current().AddDays(1));
        
        //Assert
        trip.Tickets.Should().HaveCount(1);
        passenger.Tickets.Should().HaveCount(1);
    }
    
    [Fact]
    public void  ReserveTicket_GivenInvalidTripDate_ShouldFail()
    {
        //Arrange
        var trip = InitTrip();
        var passenger = InitPassenger();
        
        //Act
        var exception = Record.Exception(() => trip.ReserveTicket(passenger, _station1, _station3, _clock.Current().AddDays(5)));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TrainDoesNotRunOnGivenDateException>();
    }
    
    [Fact]
    public void ReserveTicket_GivenInvalidStation_ShouldFail()
    {
        //Arrange
        var trip = InitTrip();
        var passenger = InitPassenger();
        
        //Act
        var exception = Record.Exception(() => trip.ReserveTicket(passenger, _station1, _station4, _clock.Current().AddDays(1)));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<StationDoesNotExistOnScheduleException>();
    }
    
    [Fact]
    public void ReserveTicket_GivenNoAvailableSeats_ShouldFail()
    {
        //Arrange
        var trip = InitTrip();
        var passenger = InitPassenger();
        
        trip.ReserveTicket(passenger, _station1, _station3, _clock.Current().AddDays(1));
        trip.ReserveTicket(passenger, _station1, _station3, _clock.Current().AddDays(1));
        
        //Act
        var exception = Record.Exception(() => trip.ReserveTicket(passenger, _station1, _station3, _clock.Current().AddDays(1)));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<NoSeatsAvailableForReservationException>();
    }
    
    [Fact]
    public void ReserveTicket_GivenNoAvailableSeat_ShouldFail()
    {
        //Arrange
        var trip = InitTrip();
        var passenger = InitPassenger();
        var seat = trip.Train.Seats.First();
        
        trip.ReserveTicket(passenger, _station1, _station3, _clock.Current().AddDays(1), seat);
        
        //Act
        var exception = Record.Exception(() => trip.ReserveTicket(passenger, _station1, _station3, _clock.Current().AddDays(1), seat));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<SeatNoAvailableException>();
    }
    
    [Theory]
    [MemberData(nameof(InvalidGuidData))]
    public void TripId_Create_GivenInvalidTripId_ShouldFail(Guid tripId)
    {
        //Arrange
        //Act
        var exception = Record.Exception(() => new TripId(tripId));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<InvalidEntityIdException>();
    }
    
    [Theory]
    [MemberData(nameof(ValidGuidData))]
    public void TripId_Create_GivenValidTripId_ShouldSucceed(Guid tripId)
    {
        //Arrange
        //Act
        var result = new TripId(tripId);
        
        //Assert
        result.Should().NotBeNull();
        result.Value.Should().Be(tripId);
    }


    #region ARRANGE

    private static readonly TripId TripId = TripId.Create();
    private readonly IClock _clock;
    private readonly Passenger _passenger;
    private readonly Carrier _carrier;
    private readonly Train _train;
    private readonly Station _station1;
    private readonly Station _station2;
    private readonly Station _station3;
    private readonly Station _station4;
    private readonly List<StationSchedule> _stationSchedules;
    private readonly Schedule _schedule;
    private readonly Trip _trip;

    public TripTests()
    {
        _clock = new TestClock();

        _passenger = InitPassenger();
             
        _carrier = Carrier.Create(
            id: Guid.NewGuid(),
            name: "Good trains");
        
        _train = Train.Create(
            id: Guid.NewGuid(),
            name: $"{_carrier.Name.Value.ToLower().Underscore()}_{new Random().Next(1000, 10000)}",
            seatsAmount: 2,
            carrier: _carrier);

        _carrier.AddTrain(_train);
        
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

        _station4 = Station.Create(
            id: Guid.NewGuid(),
            name: "Zakopane",
            city: "Zakopane",
            numberOfPlatforms: 4);

        _stationSchedules = new List<StationSchedule>
        {
            StationSchedule.Create(_station1, new TimeOnly(9, 00), new TimeOnly(9, 00), 2),
            StationSchedule.Create(_station2, new TimeOnly(10, 00), new TimeOnly(10, 05), 1),
            StationSchedule.Create(_station3, new TimeOnly(12, 15), new TimeOnly(12, 15), 1),
        };

        var tripStationSchedules = _stationSchedules.OrderBy(x => x.DepartureTime);
        
        _schedule = Schedule.Create(
            tripId: TripId,
            validDate: new ValidDate(DateOnly.FromDateTime(_clock.Current()), DateOnly.FromDateTime(_clock.Current().AddMonths(3))),
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

        _trip = InitTrip();
    }
    
    private Trip InitTrip()
        => Trip.Create(
            id: TripId,
            price: 25.00M,
            train: _train,
            schedule: _schedule);
    
    private Passenger InitPassenger()
        => Passenger.Create(
            id: Guid.NewGuid(),
            firstName: "John",
            lastName: "Doe",
            email: "johndoe@mail.com",
            dateOfBirth: new DateOnly(1990, 5, 24),
            password: "Password123!");
    
    public static IEnumerable<object[]> InvalidGuidData()
    {
        yield return new object[] { Guid.Empty };
        yield return new object[] {null};
        yield return new object[] { Guid.Parse("00000000-0000-0000-0000-000000000000") };
    }
    
    public static IEnumerable<object[]> ValidGuidData()
    {
        yield return new object[] { Guid.NewGuid() };
        yield return new object[] { Guid.Parse("00000000-0000-0000-0000-000000000001") };
    }
    
    #endregion
}