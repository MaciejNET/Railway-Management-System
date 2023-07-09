using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Station;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;
using RailwayManagementSystem.Core.ValueObjects;

namespace RailwayManagementSystem.UnitTests.Commands;

public class DeleteStationTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_StationDeletedSuccessfully()
    {
        //Arrange
        var stationId = Guid.NewGuid();
        var command = new DeleteStation(stationId);
        var stationRepositoryMock = new Mock<IStationRepository>();
        var stationScheduleRepositoryMock = new Mock<IStationScheduleRepository>();
        
        var station = Station.Create(new StationId(stationId), new StationName("Test"), new City("Test"), 2);

        stationRepositoryMock
            .Setup(repo => repo.GetByIdAsync(stationId))
            .ReturnsAsync(station);

        stationScheduleRepositoryMock
            .Setup(repo => repo.IsAnyScheduleInStation(station))
            .ReturnsAsync(false);
        
        var deleteStationHandler = new DeleteStationHandler(stationRepositoryMock.Object, stationScheduleRepositoryMock.Object);
        
        //Act
        await deleteStationHandler.HandleAsync(command);
        
        //Assert
        stationRepositoryMock.Verify(repo => repo.GetByIdAsync(stationId), Times.Once);
        stationScheduleRepositoryMock.Verify(repo => repo.IsAnyScheduleInStation(station), Times.Once);
        stationRepositoryMock.Verify(repo => repo.DeleteAsync(station), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_StationNotFound_ThrowsStationNotFoundException()
    {
        //Arrange
        var command = new DeleteStation(Guid.NewGuid());
        var stationRepositoryMock = new Mock<IStationRepository>();
        var stationScheduleRepositoryMock = new Mock<IStationScheduleRepository>();
        
        stationRepositoryMock
            .Setup(repo => repo.GetByIdAsync(command.Id))
            .ReturnsAsync((Station)null);
        
        var deleteStationHandler = new DeleteStationHandler(stationRepositoryMock.Object, stationScheduleRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => deleteStationHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<StationNotFoundException>();
    }

    [Fact]
    public async Task HandleAsync_StationHasSchedule_ThrowsStationScheduleExistsException()
    {
        //Arrange
        var stationId = Guid.NewGuid();
        var command = new DeleteStation(stationId);
        var stationRepositoryMock = new Mock<IStationRepository>();
        var stationScheduleRepositoryMock = new Mock<IStationScheduleRepository>();
        
        var station = Station.Create(new StationId(stationId), new StationName("Test"), new City("Test"), 2);

        stationRepositoryMock
            .Setup(repo => repo.GetByIdAsync(stationId))
            .ReturnsAsync(station);

        stationScheduleRepositoryMock
            .Setup(repo => repo.IsAnyScheduleInStation(station))
            .ReturnsAsync(true);
        
        var deleteStationHandler = new DeleteStationHandler(stationRepositoryMock.Object, stationScheduleRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => deleteStationHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<StationScheduleExistsException>();
    }
}