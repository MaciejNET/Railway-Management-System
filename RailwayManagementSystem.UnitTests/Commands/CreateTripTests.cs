using FluentAssertions;
using Microsoft.Extensions.Time.Testing;
using Moq;
using RailwayManagementSystem.Application.Commands.Trip;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class CreateTripTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_TripCreatedSuccessfully()
    {
        var command = new CreateTrip(
            Id: Guid.NewGuid(),
            Price: 12.33M,
            TrainName: "Test",
            Schedule: new ScheduleWriteModel(
                ValidDate: new ValidDateWriteModel(DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime),
                    DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime.AddMonths(1))),
                TripAvailability: new TripAvailabilityWriteModel(true, true, true, true, true, false, false),
                Stations:
                [
                    new("Station A", new TimeOnly(10, 00, 00), new TimeOnly(10, 10, 00), 1),
                    new("Station B", new TimeOnly(11, 00, 00), new TimeOnly(11, 10, 00), 1),
                    new("Station C", new TimeOnly(12, 00, 00), new TimeOnly(12, 10, 00), 1)
                ]));
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var trainRepositoryMock = new Mock<ITrainRepository>();
        
        tripRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Trip>()))
            .Verifiable();
        
        trainRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.TrainName))
            .ReturnsAsync(Train.Create(
                id: new TrainId(Guid.NewGuid()),
                name: new TrainName(command.TrainName),
                seatsAmount: 123,
                carrier: Carrier.Create(id: new CarrierId(Guid.NewGuid()), name: new CarrierName("Carrier"))));

        stationRepositoryMock
            .SetupSequence(repo => repo.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync(Station.Create(
                id: new StationId(Guid.NewGuid()),
                name: new StationName("Station A"),
                city: new City("City A"),
                numberOfPlatforms: 2))
            .ReturnsAsync(Station.Create(
                id: new StationId(Guid.NewGuid()),
                name: new StationName("Station B"),
                city: new City("City B"),
                numberOfPlatforms: 2))
            .ReturnsAsync(Station.Create(
                id: new StationId(Guid.NewGuid()),
                name: new StationName("Station C"),
                city: new City("City C"),
                numberOfPlatforms: 2));
        
        var createTripHandler = new CreateTripHandler(tripRepositoryMock.Object, stationRepositoryMock.Object, trainRepositoryMock.Object);
        
        //Act
        await createTripHandler.HandleAsync(command);
        
        //Assert
        tripRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Trip>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_InvalidCommand_ThrowsTripStationsCountException()
    {
        var command = new CreateTrip(
            Id: Guid.NewGuid(),
            Price: 12.33M,
            TrainName: "Test",
            Schedule: new ScheduleWriteModel(
                ValidDate: new ValidDateWriteModel(DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime),
                    DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime.AddMonths(1))),
                TripAvailability: new TripAvailabilityWriteModel(true, true, true, true, true, false, false),
                Stations: []));
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var trainRepositoryMock = new Mock<ITrainRepository>();
        
        var createTripHandler = new CreateTripHandler(tripRepositoryMock.Object, stationRepositoryMock.Object, trainRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createTripHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TripStationsCountException>();
    }

    [Fact]
    public async Task HandleAsync_TrainNotFound_ThrowsTrainNotFoundException()
    {
        var command = new CreateTrip(
            Id: Guid.NewGuid(),
            Price: 12.33M,
            TrainName: "Test",
            Schedule: new ScheduleWriteModel(
                ValidDate: new ValidDateWriteModel(DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime),
                    DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime.AddMonths(1))),
                TripAvailability: new TripAvailabilityWriteModel(true, true, true, true, true, false, false),
                Stations:
                [
                    new("Station A", new TimeOnly(10, 00, 00), new TimeOnly(10, 10, 00), 1),
                    new("Station B", new TimeOnly(11, 00, 00), new TimeOnly(11, 10, 00), 1),
                    new("Station C", new TimeOnly(12, 00, 00), new TimeOnly(12, 10, 00), 1)
                ]));
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var trainRepositoryMock = new Mock<ITrainRepository>();
        
        trainRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.TrainName))
            .ReturnsAsync((Train)null);
        
        var createTripHandler = new CreateTripHandler(tripRepositoryMock.Object, stationRepositoryMock.Object, trainRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createTripHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<TrainNotFoundException>();  
    }

    [Fact]
    public async Task HandleAsync_StationNotFound_ThrowsStationNotFoundException()
    {
        var command = new CreateTrip(
            Id: Guid.NewGuid(),
            Price: 12.33M,
            TrainName: "Test",
            Schedule: new ScheduleWriteModel(
                ValidDate: new ValidDateWriteModel(DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime),
                    DateOnly.FromDateTime(_timeProvider.GetUtcNow().DateTime.AddMonths(1))),
                TripAvailability: new TripAvailabilityWriteModel(true, true, true, true, true, false, false),
                Stations:
                [
                    new("Station A", new TimeOnly(10, 00, 00), new TimeOnly(10, 10, 00), 1),
                    new("Station B", new TimeOnly(11, 00, 00), new TimeOnly(11, 10, 00), 1),
                    new("Station C", new TimeOnly(12, 00, 00), new TimeOnly(12, 10, 00), 1)
                ]));
        
        var tripRepositoryMock = new Mock<ITripRepository>();
        var stationRepositoryMock = new Mock<IStationRepository>();
        var trainRepositoryMock = new Mock<ITrainRepository>();

        trainRepositoryMock
            .Setup(repo => repo.GetByNameAsync(command.TrainName))
            .ReturnsAsync(Train.Create(
                id: new TrainId(Guid.NewGuid()),
                name: new TrainName(command.TrainName),
                seatsAmount: 123,
                carrier: Carrier.Create(id: new CarrierId(Guid.NewGuid()), name: new CarrierName("Carrier"))));
        
        stationRepositoryMock
            .SetupSequence(repo => repo.GetByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((Station)null)
            .ReturnsAsync(Station.Create(
                id: new StationId(Guid.NewGuid()),
                name: new StationName("Station B"),
                city: new City("City B"),
                numberOfPlatforms: 2))
            .ReturnsAsync(Station.Create(
                id: new StationId(Guid.NewGuid()),
                name: new StationName("Station C"),
                city: new City("City C"),
                numberOfPlatforms: 2));
        
        var createTripHandler = new CreateTripHandler(tripRepositoryMock.Object, stationRepositoryMock.Object, trainRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createTripHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<StationNotFoundException>();
    }

    #region ARRANGE

    private readonly FakeTimeProvider _timeProvider;

    public CreateTripTests()
    {
        _timeProvider = new FakeTimeProvider();
        _timeProvider.SetUtcNow(new DateTime(2023, 7, 10, 12, 0, 0));
    }

    #endregion
}