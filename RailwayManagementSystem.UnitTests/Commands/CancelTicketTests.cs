using FluentAssertions;
using Humanizer;
using Moq;
using RailwayManagementSystem.Application.Commands.Ticket;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Abstractions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;
using RailwayManagementSystem.UnitTests.Shared;

namespace RailwayManagementSystem.UnitTests.Commands;

public class CancelTicketTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_TicketCanceledSuccessfully()
    {
        //Arrange
        var command = new CancelTicket(Guid.NewGuid(), PassengerId);
        
        var ticketRepositoryMock = new Mock<ITicketRepository>();

        var ticket = new Ticket(_trip, PassengerId, 12.33M, _seat, _clock.Current().AddDays(1),
            new List<Station> {_station1, _station2, _station3});

        ticketRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync(ticket);
        
        var cancelTicketHandler = new CancelTicketHandler(ticketRepositoryMock.Object);
        
        //Act
        await cancelTicketHandler.HandleAsync(command);
        
        //Assert
        ticketRepositoryMock.Verify(repo => repo.GetByIdAsync(command.Id), Times.Once);
        ticketRepositoryMock.Verify(repo => repo.DeleteAsync(ticket), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_TicketNotFound_ThrowsTicketNotFoundException()
    {
        //Arrange
        var command = new CancelTicket(Guid.NewGuid(), PassengerId);
        
        var ticketRepositoryMock = new Mock<ITicketRepository>();
        
        ticketRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync((Ticket)null);
        
        var cancelTicketHandler = new CancelTicketHandler(ticketRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => cancelTicketHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TicketNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_TicketNotAssignedToPassenger_ThrowsTicketNotAssignToThePassengerException()
    {
        //Arrange
        var command = new CancelTicket(Guid.NewGuid(), PassengerId);
        
        var ticketRepositoryMock = new Mock<ITicketRepository>();

        var ticket = new Ticket(_trip, new UserId(Guid.NewGuid()), 12.33M, _seat, _clock.Current().AddDays(1),
            new List<Station> {_station1, _station2, _station3});

        ticketRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync(ticket);
        
        var cancelTicketHandler = new CancelTicketHandler(ticketRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => cancelTicketHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TicketNotAssignToThePassengerException>();
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

    public CancelTicketTests()
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
                Sunday: false),
            stations: _stationSchedules);

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