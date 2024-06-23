using FluentAssertions;
using Humanizer;
using Microsoft.Extensions.Time.Testing;
using Moq;
using RailwayManagementSystem.Application.Commands.Trip;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class DeleteTripTests
{
    [Fact]
    public async Task HandleAsync_ExistingTrip_TripDeletedSuccessfully()
    {
        //Arrange
        var tripId = TripId.Value;
        var command = new DeleteTrip(tripId);

        var tripRepositoryMock = new Mock<ITripRepository>();
        
        tripRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(InitTrip());
        
        var deleteTripHandler = new DeleteTripHandler(tripRepositoryMock.Object);
        
        //Act
        await deleteTripHandler.HandleAsync(command);
        
        //Assert
        tripRepositoryMock.Verify(repo => repo.DeleteAsync(It.IsAny<Trip>()), Times.Once);
    }
    
    [Fact]
    public async Task HandleAsync_NonExistingTrip_ThrowsTripNotFoundException()
    {
        //Arrange
        var tripId = TripId.Value;
        var command = new DeleteTrip(tripId);

        var tripRepositoryMock = new Mock<ITripRepository>();
        
        tripRepositoryMock
            .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Trip)null);
        
        var deleteTripHandler = new DeleteTripHandler(tripRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => deleteTripHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TripNotFoundException>();
    }
    
    #region ARRANGE

    private static readonly TripId TripId = TripId.Create();
    private static readonly UserId PassengerId = UserId.Create();
    private readonly FakeTimeProvider _timeProvider;
    private readonly Passenger _passenger;
    private readonly Carrier _carrier;
    private readonly Train _train;
    private readonly Station _station1;
    private readonly Station _station2;
    private readonly Station _station3;
    private readonly List<StationSchedule> _stationSchedules;
    private readonly Schedule _schedule;
    private readonly Seat _seat;
    private readonly Trip _trip;

    public DeleteTripTests()
    {
        _timeProvider = new FakeTimeProvider();
        _timeProvider.SetUtcNow(new DateTime(2023, 7, 10, 12, 0, 0));

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
        
        _stationSchedules = 
        [
            StationSchedule.Create(_station1, new TimeOnly(9, 00), new TimeOnly(9, 00), 2),
            StationSchedule.Create(_station2, new TimeOnly(10, 00), new TimeOnly(10, 05), 1),
            StationSchedule.Create(_station3, new TimeOnly(12, 15), new TimeOnly(12, 15), 1),
        ];

        var tripStationSchedules = _stationSchedules.OrderBy(x => x.DepartureTime);
        
        _schedule = Schedule.Create(
            tripId: TripId,
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

        _trip = InitTrip();
        _seat = _train.Seats.First();
    }
    
    private Trip InitTrip()
        => Trip.Create(
            id: TripId,
            price: 25.00M,
            train: _train,
            schedule: _schedule);
    
    private Passenger InitPassenger()
        => Passenger.Create(
            id: PassengerId,
            firstName: "John",
            lastName: "Doe",
            email: "johndoe@mail.com",
            dateOfBirth: new DateOnly(1990, 5, 24),
            password: "Password123!");

    #endregion
}