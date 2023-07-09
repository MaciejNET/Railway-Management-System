using FluentAssertions;
using Moq;
using RailwayManagementSystem.Application.Commands.Station;
using RailwayManagementSystem.Application.Exceptions;
using RailwayManagementSystem.Core.Entities;
using RailwayManagementSystem.Core.Repositories;

namespace RailwayManagementSystem.UnitTests.Commands;

public class CreateStationTests
{
    [Fact]
    public async Task HandleAsync_ValidCommand_StationAddedSuccessfully()
    {
        //Arrange
        var command = new CreateStation(Guid.NewGuid(), "Station", "City", 2);
        var stationRepositoryMock = new Mock<IStationRepository>();

        stationRepositoryMock
            .Setup(repo => repo.ExistsByNameAsync(command.Name))
            .ReturnsAsync(false);
        
        var createStationHandler = new CreateStationHandler(stationRepositoryMock.Object);
        
        //Act
        await createStationHandler.HandleAsync(command);
        
        //Assert
        stationRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Station>()), Times.Once);
        stationRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Station>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_StationAlreadyExists_ThrowsStationWithGivenNameAlreadyExistsException()
    {
        //Arrange
        var command = new CreateStation(Guid.NewGuid(), "Station", "City", 2);
        var stationRepositoryMock = new Mock<IStationRepository>();
        
        stationRepositoryMock
            .Setup(repo => repo.ExistsByNameAsync(command.Name))
            .ReturnsAsync(true);
        
        var createStationHandler = new CreateStationHandler(stationRepositoryMock.Object);
        
        //Act
        var exception = await Record.ExceptionAsync(() => createStationHandler.HandleAsync(command));
        
        //Assert
        exception.Should().NotBeNull();
        exception.Should().BeOfType<StationWithGivenNameAlreadyExistsException>();
    }
}