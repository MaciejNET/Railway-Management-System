using FluentAssertions;
using FluentAssertions.Common;
using Humanizer;
using Moq;
using RailwayManagementSystem.Application.Commands.Ticket;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;
using RailwayManagementSystem.UnitTests.Shared;

namespace RailwayManagementSystem.UnitTests.Commands;

public class BookTicketTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_TicketBookedSuccessfully()
    {
        //Arrange
        var command = new BookTicket(TripId, PassengerId, _clock.Current(), "Starachowice", "Krakow Glowny", _seat.Id);
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var seatRepositoryMock = new Mock<ISeatRepository>();

        var trip = InitTrip();
        var passenger = InitPassenger();

        tripRepositoryMock
            .Setup(repo => repo.GetByIdAsync(TripId))
            .ReturnsAsync(trip);

        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(PassengerId))
            .ReturnsAsync(passenger);

        stationRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.StartStation))
            .ReturnsAsync(_station1);

        stationRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.EndStation))
            .ReturnsAsync(_station3);
    
        seatRepositoryMock
            .Setup(repo => repo.GetByIdAsync(_seat.Id))
            .ReturnsAsync(_seat);

        var bookTickerHandler = new BookTicketHandler(
            tripRepositoryMock.Object,
            passengerRepositoryMock.Object,
            stationRepositoryMock.Object,
            seatRepositoryMock.Object);
        
        //Act
        await bookTickerHandler.HandleAsync(command);
        
        //Assert
        trip.Tickets.Should().HaveCount(1);
        passenger.Tickets.Should().HaveCount(1);
        tripRepositoryMock.Verify(repo => repo.GetByIdAsync(TripId), Times.Once);
        passengerRepositoryMock.Verify(repo => repo.GetByIdAsync(PassengerId), Times.Once);
        stationRepositoryMock.Verify(repo => repo.GetByNameAsync(command.StartStation), Times.Once);
        stationRepositoryMock.Verify(repo => repo.GetByNameAsync(command.EndStation), Times.Once);
        seatRepositoryMock.Verify(repo => repo.GetByIdAsync(_seat.Id), Times.Once);
        tripRepositoryMock.Verify(repo => repo.UpdateAsync(trip), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_TripNotFound_ThrowsTripNotFoundException()
    {
        var command = new BookTicket(TripId, PassengerId, _clock.Current(), "Starachowice", "Krakow Glowny", _seat.Id);
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var seatRepositoryMock = new Mock<ISeatRepository>();
        
        tripRepositoryMock
            .Setup(repo => repo.GetByIdAsync(TripId))
            .ReturnsAsync((Trip)null);
        
        var bookTickerHandler = new BookTicketHandler(
            tripRepositoryMock.Object,
            passengerRepositoryMock.Object,
            stationRepositoryMock.Object,
            seatRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => bookTickerHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TripNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_PassengerNotFound_ThrowsPassengerNotFoundException()
    {
        //Arrange
        var command = new BookTicket(TripId, PassengerId, _clock.Current(), "Starachowice", "Krakow Glowny", _seat.Id);
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var seatRepositoryMock = new Mock<ISeatRepository>();

        var trip = InitTrip();

        tripRepositoryMock
            .Setup(repo => repo.GetByIdAsync(TripId))
            .ReturnsAsync(trip);

        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(PassengerId))
            .ReturnsAsync((Passenger)null);
        
        var bookTickerHandler = new BookTicketHandler(
            tripRepositoryMock.Object,
            passengerRepositoryMock.Object,
            stationRepositoryMock.Object,
            seatRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => bookTickerHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<PassengerNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_StartStationNotFound_ThrowsStationNotFoundException()
    {
        //Arrange
        var command = new BookTicket(TripId, PassengerId, _clock.Current(), "Starachowice", "Krakow Glowny", _seat.Id);
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var seatRepositoryMock = new Mock<ISeatRepository>();

        var trip = InitTrip();
        var passenger = InitPassenger();

        tripRepositoryMock
            .Setup(repo => repo.GetByIdAsync(TripId))
            .ReturnsAsync(trip);

        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(PassengerId))
            .ReturnsAsync(passenger);

        stationRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.StartStation))
            .ReturnsAsync((Station)null);

        var bookTickerHandler = new BookTicketHandler(
            tripRepositoryMock.Object,
            passengerRepositoryMock.Object,
            stationRepositoryMock.Object,
            seatRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => bookTickerHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<StationNotFoundException>();
    }
    
    [Fact]
    public async Task HandleAsync_EndStationNotFound_ThrowsStationNotFoundException()
    {
        //Arrange
        var command = new BookTicket(TripId, PassengerId, _clock.Current(), "Starachowice", "Krakow Glowny", _seat.Id);
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var seatRepositoryMock = new Mock<ISeatRepository>();

        var trip = InitTrip();
        var passenger = InitPassenger();

        tripRepositoryMock
            .Setup(repo => repo.GetByIdAsync(TripId))
            .ReturnsAsync(trip);

        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(PassengerId))
            .ReturnsAsync(passenger);

        stationRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.StartStation))
            .ReturnsAsync(_station1);
        
        stationRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.EndStation))
            .ReturnsAsync((Station)null);

        var bookTickerHandler = new BookTicketHandler(
            tripRepositoryMock.Object,
            passengerRepositoryMock.Object,
            stationRepositoryMock.Object,
            seatRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => bookTickerHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<StationNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_SeatNotFound_ThrowsSeatNotFoundException()
    {
        //Arrange
        var command = new BookTicket(TripId, PassengerId, _clock.Current(), "Starachowice", "Krakow Glowny", _seat.Id);
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var passengerRepositoryMock = new Mock<IPassengerRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var seatRepositoryMock = new Mock<ISeatRepository>();

        var trip = InitTrip();
        var passenger = InitPassenger();

        tripRepositoryMock
            .Setup(repo => repo.GetByIdAsync(TripId))
            .ReturnsAsync(trip);

        passengerRepositoryMock
            .Setup(repo => repo.GetByIdAsync(PassengerId))
            .ReturnsAsync(passenger);

        stationRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.StartStation))
            .ReturnsAsync(_station1);

        stationRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.EndStation))
            .ReturnsAsync(_station3);
    
        seatRepositoryMock
            .Setup(repo => repo.GetByIdAsync(_seat.Id))
            .ReturnsAsync((Seat)null);

        var bookTickerHandler = new BookTicketHandler(
            tripRepositoryMock.Object,
            passengerRepositoryMock.Object,
            stationRepositoryMock.Object,
            seatRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => bookTickerHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<SeatNotFoundException>();
    }

    #region ARRANGE

    private static readonly TripId TripId = TripId.Create();
    private static readonly UserId PassengerId = UserId.Create();
    private readonly Core.Abstractions.IClock _clock;
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

    public BookTicketTests()
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